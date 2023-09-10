using MyJenga.Jenga.Scripts;
using MyJenga.Jenga.Scripts.Interfaces;
using MyJenga.UI.Scripts;
using MyJenga.UI.Scripts.Interfaces;
using UnityEngine;
using Zenject;

namespace MyJenga.Core.Scripts
{
    using BlockType = JengaBlock.BlockType;

    public class ApplicationManager : MonoBehaviour
    {
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private TextAsset _inputData;

        [Header("Tower positions")]
        [SerializeField] private Vector3 _sixthGradeTowerPosition;
        [SerializeField] private Vector3 _seventhGradeTowerPosition;
        [SerializeField] private Vector3 _eighthGradeTowerPosition;

        private ILeftToolbarUI _leftToolbarUI;
        private IRightToolbarUI _rightToolbarUI;
        private IInfoPanelUI _infoPanelUI;
        private IHelpPanelUI _helpPanelUI;
        private IQuitPanelUI _quitPanelUI;
        private IJengaTowerManager _towerManager;

        private TowerGrade _currentTower;
        private JengaBlock _currentSelectedBlock = null;
        private bool _uiOpen;

        [Inject]
        public void Init(
            ILeftToolbarUI leftToolbarUI,
            IRightToolbarUI rightToolbarUI,
            IInfoPanelUI infoPanelUI,
            IHelpPanelUI helpPanelUI,
            IQuitPanelUI quitPanelUI,
            IJengaTowerManager towerManager)
        {
            _leftToolbarUI = leftToolbarUI;
            _rightToolbarUI = rightToolbarUI;
            _infoPanelUI = infoPanelUI;
            _helpPanelUI = helpPanelUI;
            _quitPanelUI = quitPanelUI;
            _towerManager = towerManager;
        }

        private void Start()
        {
            // Construct towers using url data
            _towerManager.LoadData(_inputData);

            _towerManager.BuildTower(TowerGrade.Grade6, _sixthGradeTowerPosition);
            _towerManager.BuildTower(TowerGrade.Grade7, _seventhGradeTowerPosition);
            _towerManager.BuildTower(TowerGrade.Grade8, _eighthGradeTowerPosition);

            // Setup camera controller actions
            _cameraController.TransitionStateChanged += OnCameraTransitionStateChanged;

            // Setup UI actions
            _leftToolbarUI.SixthGradeToggle.onValueChanged.AddListener(delegate (bool value) { TowerToggled(value, TowerGrade.Grade6); });
            _leftToolbarUI.SeventhGradeToggle.onValueChanged.AddListener(delegate (bool value) { TowerToggled(value, TowerGrade.Grade7); });
            _leftToolbarUI.EighthGradeToggle.onValueChanged.AddListener(delegate (bool value) { TowerToggled(value, TowerGrade.Grade8); });

            _rightToolbarUI.TestStackButton.onClick.AddListener(TestCurrentTower);
            _rightToolbarUI.ResetButton.onClick.AddListener(ResetCurrentTower);
            _rightToolbarUI.HelpButton.onClick.AddListener(delegate { ShowHelpUI(true); });
            _rightToolbarUI.QuitButton.onClick.AddListener(delegate { ShowQuitUI(true); });

            _helpPanelUI.OkButton.onClick.AddListener(delegate { ShowHelpUI(false); });

            _quitPanelUI.NoButton.onClick.AddListener(delegate { ShowQuitUI(false); });
            _quitPanelUI.YesButton.onClick.AddListener(ExitApplication);

            // Setup initial UI layout
            _infoPanelUI.SetActive(false);
            _helpPanelUI.SetActive(false);
            _quitPanelUI.SetActive(false);

            _leftToolbarUI.SeventhGradeToggle.isOn = true; // start from 7th grade position
        }

        private void OnDestroy()
        {
            _cameraController.TransitionStateChanged -= OnCameraTransitionStateChanged;

            _leftToolbarUI.SixthGradeToggle.onValueChanged.RemoveListener(delegate (bool value) { TowerToggled(value, TowerGrade.Grade6); });
            _leftToolbarUI.SeventhGradeToggle.onValueChanged.RemoveListener(delegate (bool value) { TowerToggled(value, TowerGrade.Grade7); });
            _leftToolbarUI.EighthGradeToggle.onValueChanged.RemoveListener(delegate (bool value) { TowerToggled(value, TowerGrade.Grade8); });

            _rightToolbarUI.TestStackButton.onClick.RemoveListener(TestCurrentTower);
            _rightToolbarUI.ResetButton.onClick.RemoveListener(ResetCurrentTower);
            _rightToolbarUI.HelpButton.onClick.RemoveListener(delegate { ShowHelpUI(true); });
            _rightToolbarUI.QuitButton.onClick.RemoveListener(delegate { ShowQuitUI(true); });

            _helpPanelUI.OkButton.onClick.RemoveListener(delegate { ShowHelpUI(false); });

            _quitPanelUI.NoButton.onClick.RemoveListener(delegate { ShowQuitUI(false); });
            _quitPanelUI.YesButton.onClick.RemoveListener(ExitApplication);
        }

        private void Update()
        {
            // Disable all controls during transitions or with a UI open
            if (_cameraController.IsTransitioning || _uiOpen)
            {
                return;
            }

            KeyboardControls();
            MouseControls();
        }

        private void KeyboardControls()
        {
            if (Input.GetKey(KeyCode.Alpha1))
            {
                _leftToolbarUI.SixthGradeToggle.isOn = true;
            }
            else if (Input.GetKey(KeyCode.Alpha2))
            {
                _leftToolbarUI.SeventhGradeToggle.isOn = true;
            }
            else if (Input.GetKey(KeyCode.Alpha3))
            {
                _leftToolbarUI.EighthGradeToggle.isOn = true;
            }
            else if (Input.GetKey(KeyCode.T))
            {
                TestCurrentTower();
            }
            else if (Input.GetKey(KeyCode.R))
            {
                ResetCurrentTower();
            }
            else if (Input.GetKey(KeyCode.H))
            {
                ShowHelpUI(true);
            }
            else if (Input.GetKey(KeyCode.Q))
            {
                ShowQuitUI(true);
            }
        }

        private void MouseControls()
        {
            // Check for right-mouse click on a jenga block
            if (Input.GetMouseButtonDown(1) && !UIStatics.IsMouseOverUI())
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit raycastHit, 200.0f))
                {
                    if (raycastHit.transform != null && raycastHit.collider.CompareTag("JengaBlock"))
                    {
                        JengaBlock block = raycastHit.collider.gameObject.GetComponent<JengaBlock>();
                        SelectBlock(block);
                    }
                }
            }
        }

        private void SelectBlock(JengaBlock block)
        {
            if (_currentSelectedBlock != null)
            {
                _currentSelectedBlock.SetOutlineEnabled(false);
            }

            if (GameObject.ReferenceEquals(block, _currentSelectedBlock))
            {
                _currentSelectedBlock = null;
                _infoPanelUI.SetActive(false);
            }
            else
            {
                _currentSelectedBlock = block;
                _currentSelectedBlock.SetOutlineEnabled(true);
                _infoPanelUI.SetActive(true);

                if (_towerManager.TryGetDataForBlock(block.Id, out GradesData data))
                {
                    string description = string.Format(
                        "\n{0}: {1}\n\n{2}\n\n{3}: {4}\n",
                        data.grade, data.domain, data.cluster, data.standardId, data.standardDescription);

                    _infoPanelUI.SetText(description);
                }
            }
        }

        private void SelectTower(TowerGrade tower)
        {
            _currentTower = tower;

            Vector3 towerPosition = GetCurrentTowerPosition();

            // Add a bit of height to the camera
            Vector3 position = towerPosition + new Vector3(0.0f, 10.0f, 0.0f);
            Quaternion rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);

            _cameraController.SetupOrbit(position, rotation);
        }

        private Vector3 GetCurrentTowerPosition()
        {
            return _currentTower switch
            {
                TowerGrade.Grade6 => _sixthGradeTowerPosition,
                TowerGrade.Grade7 => _seventhGradeTowerPosition,
                TowerGrade.Grade8 => _eighthGradeTowerPosition,
                _ => new Vector3()
            };
        }

        private void OnCameraTransitionStateChanged(bool value)
        {
            _leftToolbarUI.SetTogglesEnabled(!value);
            _rightToolbarUI.SetButtonsEnabled(!value);
        }

        private void TowerToggled(bool value, TowerGrade tower)
        {
            if (value && _currentTower != tower)
            {
                SelectTower(tower);
            }
        }

        private void TestCurrentTower()
        {
            _towerManager.SetTowerBlocksActive(_currentTower, BlockType.Glass, false);
        }

        private void ResetCurrentTower()
        {
            _towerManager.BuildTower(_currentTower, GetCurrentTowerPosition());
        }

        private void ShowHelpUI(bool open)
        {
            if (!(open ^ _uiOpen))
            {
                return;
            }

            _uiOpen = open;
            _helpPanelUI.SetActive(open);
        }

        private void ShowQuitUI(bool open)
        {
            if (!(open ^ _uiOpen))
            {
                return;
            }

            _uiOpen = open;
            _quitPanelUI.SetActive(open);
        }

        private void ExitApplication()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
        }
    }
}