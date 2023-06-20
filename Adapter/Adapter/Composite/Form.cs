using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter.Composite
{
    public class Form : ILeaf
    {
        string name;

        List<ILeaf> children;

        public Form(string name)
        {
            this.name = name;
        }

        public void AddComponent(ILeaf leaf)
        {
            if (children == null) children = new List<ILeaf>();

            children.Add(leaf);
        }

        public string ConvertToString()
        {
            var stringBuilder = new StringBuilder($"<form name='{name}'>");

            foreach (var child in children)
            {
                stringBuilder.Append("\n" + child.ConvertToString());
            }
            stringBuilder.Append("\n</form>");

            return stringBuilder.ToString();
        }
    }
}
