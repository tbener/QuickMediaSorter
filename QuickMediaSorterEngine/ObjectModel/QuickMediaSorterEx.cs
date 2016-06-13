using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using QuickMediaSorter.Helpers;
using TalUtils;

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

    public partial class QuickMediaSorterProject
    {
        [XmlIgnore]
        public Dictionary<string, QuickMediaSorterProjectTriggerAction> ActionsDict { get; set; }

        [XmlIgnore]
        public FileInfo FileInfo { get; set; }

        private List<string> _fileList = null;
        private int _current;

        public void InitializeActions()
        {
            // Create actions dictionary
            ActionsDict = new Dictionary<string, QuickMediaSorterProjectTriggerAction>();

            // Create default actions
            ActionsDict["Delete"] = new QuickMediaSorterProjectTriggerAction(ActionType.Delete);
            ActionsDict["Left"] = new QuickMediaSorterProjectTriggerAction(ActionType.Previous);
            ActionsDict["Right"] = new QuickMediaSorterProjectTriggerAction(ActionType.Next);

            // Merge the existing actions
            foreach (QuickMediaSorterProjectTrigger trigger in Trigger)
            {
                ActionsDict[trigger.Key] = trigger.Action;
            }

            // Set actions folders (also build them if necessary)
            foreach (var action in ActionsDict.Values.Where(action => !String.IsNullOrEmpty(action.Folder)))
            {
                action.FullPathFolder = PathHelper.GetFullPath(this.Folder, action.Folder, true);
            }

            if (Extentions == null) Extentions = "*.jpg";
        }

        

        public void ReadFiles()
        {
            if (_fileList == null) 
                _fileList = new List<string>();
            else
                _fileList.Clear();

            _current = -1;
            FileInfo = null;

            string[] fileEntries = Directory.GetFiles(Folder, Extentions);
            _fileList = new List<string>(fileEntries);
            SetFile(0);
        }

        internal static QuickMediaSorterProject GetDefault()
        {
            QuickMediaSorterProject qms = new QuickMediaSorterProject();

            var trigger = new QuickMediaSorterProjectTrigger();
            trigger.Key = "ControlKey";

            var act = new QuickMediaSorterProjectTriggerAction(ActionType.Copy, "Album");
            trigger.Action = act;

            qms.Trigger = new[] { trigger };

            qms.Extentions = "*.jpg";

            return qms;
        }

        private bool SetFile(int index)
        {
            if (index < 0) index = 0;
            if (index == _current) return false;
            _current = index;

            if (_current >= _fileList.Count)
            {
                FileInfo = null;
            }
            else
            {
                FileInfo = new FileInfo(_fileList[_current]);
            }

            return true;

            // we can raise event here...
        }
        
        public bool Execute(string key)
        {
            if (ActionsDict.ContainsKey(key))
            {
                var step = ActionsDict[key].Execute(FileInfo);
                return SetFile(_current + step);
            }
            return false;
        }
    }

    public partial class QuickMediaSorterProjectTrigger
    {
        //[XmlIgnore]
        //public Keys KeyCode
        //{
        //    get { return (Keys)(Enum.Parse(typeof(Keys), Key)); }
        //    set { Key = value.ToString(); }
        //}
    }

    public partial class QuickMediaSorterProjectTriggerAction
    {
        private string _fullPathFolder;

        public QuickMediaSorterProjectTriggerAction() : this(ObjectModel.ActionType.None)
        {
        }
        public QuickMediaSorterProjectTriggerAction(ActionType actionType)
        {
            ActionType = actionType.ToString();
        }

        public QuickMediaSorterProjectTriggerAction(ActionType actionType, string folder)
        {
            ActionType = actionType.ToString();
            Folder = folder;
        }

        [XmlIgnore]
        public ActionType ActionTypeEnum
        {
            get
            {
                return
                    (QuickMediaSorter.ObjectModel.ActionType)
                        Enum.Parse(typeof(ActionType), ActionType);
            }
            set { ActionType = value.ToString(); }
        }

        [XmlIgnore]
        public string FullPathFolder
        {
            get { return _fullPathFolder; }
            set { _fullPathFolder = value; }
        }


        public int Execute(FileInfo fileInfo)
        {
            return Execute(this, fileInfo);
        }

        public static int Execute(QuickMediaSorterProjectTriggerAction action, FileInfo fileInfo)
        {
            QuickMediaSorterProjectTriggerAction rolebackAction = new QuickMediaSorterProjectTriggerAction();
            int step = 0;

            switch (action.ActionTypeEnum)
            {
                case ObjectModel.ActionType.None:
                    break;
                case ObjectModel.ActionType.Next:
                    step = 1;
                    break;
                case ObjectModel.ActionType.Previous:
                    step = -1;
                    break;
                case ObjectModel.ActionType.Delete:
                    step = 1;
                    fileInfo.Delete();
                    break;
                case ObjectModel.ActionType.Copy:
                    List<string> l = new List<string>(){fileInfo.FullName};
                    ShellFileOperation.CopyItems(l, action.FullPathFolder);
                    //fileInfo.CopyTo(PathHelper.GetFullPath(action.FullPathFolder, fileInfo.Name));
                    step = 1;
                    break;
                case ObjectModel.ActionType.Move:
                    List<string> l1 = new List<string>() { fileInfo.FullName };
                    ShellFileOperation.MoveItems(l1, action.FullPathFolder);
                    //fileInfo.MoveTo(PathHelper.GetFullPath(action.FullPathFolder, fileInfo.Name));
                    step = 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }


            return step;
        }
    }
}
