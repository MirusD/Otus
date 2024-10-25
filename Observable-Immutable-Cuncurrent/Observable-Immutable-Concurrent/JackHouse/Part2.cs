using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JackHouse
{
    internal class Part2
    {
        private ImmutableList<string> poem;
        public ImmutableList<string> Poem
        {
            get => poem;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                poem = value;
            }
        }
        public void AddPart(ImmutableList<string> list)
        {
            Poem = list.Add("Который построил Джек.");
        }
    }
}
