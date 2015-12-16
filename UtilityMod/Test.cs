
using ColossalFramework.UI;
using System.Collections.Generic;
using System.IO;
namespace UtilityMod
{
    class Test : Utility
    {
        StreamWriter writer;

        protected override void Deploy()
        {
            string s = System.DateTime.Now.ToString().Replace(" ", "_").Replace("/", "-").Replace(":", "_");
            writer = new StreamWriter("walk_" + s + ".txt");
            Walk();
        }

        protected override void Revert(bool cleaningUp)
        {
            writer.Close();
            writer = null;
        }

        // A recursive walk of the UIComponent hierarchy.
        void Walk()
        {
            // UIComponent root = ToolsModifierControl.mainToolbar.component;
            UIComponent root = UIView.Find("MainToolstrip");
            root = GoUp(root);
            Walk(root, "");
        }

        UIComponent GoUp(UIComponent comp)
        {
            while (comp.parent != null)
                comp = comp.parent;

            return comp;
        }

        void Walk(UIComponent comp, string indent)
        {
            Write(comp, indent);

            if (comp != null && indent.Length < 20)
            {
                indent += "  ";
                IList<UIComponent> childs = comp.components;

                if (childs != null)
                    for (int i = 0; i < childs.Count; i++)
                        Walk(childs[i], indent);
            }
        }

        void Write(UIComponent comp, string indent)
        {
            string s = indent;

            if (comp == null)
                s += "null component";
            else
            {
                s = Append(s, comp.GetType());
                s = Append(s, comp.cachedName);
                s = Append(s, comp.isEnabled);
                s = Append(s, comp.isVisible);

                if (comp.tag != null && comp.tag == "Untagged")
                    return;

                s = Append(s, comp.tag);

                if (comp.gameObject != null)
                    s = Append(s, comp.gameObject.name);
            }

            writer.WriteLine(s);
        }

        string Append(string s, object o)
        {
            if (o == null)
                s += "null ";
            else
                s += o.ToString() + " ";

            return s;
        }
    }
}
