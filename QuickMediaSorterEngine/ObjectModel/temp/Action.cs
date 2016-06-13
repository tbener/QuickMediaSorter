using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickMediaSorter.ObjectModel
{
    public enum ActionType
    {
        None,
        Next,
        Previous,
        Delete,
        Copy,
        Move
    }

    public class Action
    {
        public ActionType ActionType { get; set; }
        public string Folder { get; set; }

        public Action()
        {
            ActionType = ActionType.None;
        }

        public Action(ActionType actionType)
        {
            ActionType = actionType;
        }

        public Action(ActionType actionType, string folder)
        {
            ActionType = actionType;
            Folder = folder;
        }

        public Action Execute(string file)
        {
            return Execute(this, file);
        }

        public static Action Execute(Action action, string file)
        {

            Action rolebackAction = new Action();
            return rolebackAction;
        }

        


    }


}
