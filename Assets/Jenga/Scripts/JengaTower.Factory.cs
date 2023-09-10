using System.Collections.Generic;
using Zenject;

namespace MyJenga.Jenga.Scripts
{
	public partial class JengaTower
	{
		public class Factory : PlaceholderFactory<IEnumerable<GradesData>, JengaTower>
		{
		}

        public class CustomFactory : IFactory<IEnumerable<GradesData>, JengaTower>
		{
			private readonly JengaBlock _blockPrefab;

            public CustomFactory(JengaBlock blockPrefab)
			{
				_blockPrefab = blockPrefab;
			}

			public JengaTower Create(IEnumerable<GradesData> dataAtGrade)
			{
				return new(_blockPrefab, dataAtGrade);
			}
		}
	}
}