using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickMediaSorter.ObjectModel
{
    public class Project
    {
        public Project()
        {
            Actions = new List<Action>();
            Extensions = new List<string>();
        }

        public string Folder { get; set; }
        public List<string> Extensions { get; set; }
        public List<Action> Actions { get; set; }
        public Dictionary<Keys, Action> ActionsDict { get; set; }

        public void MergeWithDefaults()
        {

            ActionsDict[Keys.Delete] = new Action(ActionType.Delete);
            ActionsDict[Keys.Left] = new Action(ActionType.Previous);
            ActionsDict[Keys.Right] = new Action(ActionType.Next);
        }
    }
}
