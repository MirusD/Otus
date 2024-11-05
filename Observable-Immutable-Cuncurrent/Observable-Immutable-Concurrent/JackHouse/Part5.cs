using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JackHouse
{
    internal class Part5
    {
        private ImmutableList<string> _poem;
        public ImmutableList<string> Poem
        {
            get => _poem;
            set
            {
                _poem = value ?? throw new ArgumentNullException(nameof(value));
            }
        }
        public void AddPart(ImmutableList<string> list)
        {
            Poem = list.Add("В доме,");
        }
    }
}
