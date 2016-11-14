using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace QuickMediaSorter.Classes
{
    public class KeyClass 
    {
        
        public Key Key_ { get; set; }
        public ModifierKeys Modifiers_ { get; set; }

        public KeyClass(ModifierKeys modifiers, Key key)
        {
            Key_ = key;
            Modifiers_ = modifiers;
        }

        public KeyClass() : this(ModifierKeys.None, Key.None)
        {
            
        }

        public virtual void SetKeys(ModifierKeys modifiers, Key key)
        {
            Modifiers_ = modifiers;
            Key_ = key;
        }

        public virtual void SetKeys(string modifiers, string key)
        {
            ModifierKeysConverter mod_ser = new ModifierKeysConverter();
            Modifiers_ = (ModifierKeys)mod_ser.ConvertFromString(modifiers);
            KeyConverter key_ser = new KeyConverter();
            Key_ = (Key)key_ser.ConvertFromString(key);
        }

        public override bool Equals(object obj)
        {
            KeyClass other = (KeyClass)obj;
            return this.Key_.Equals(other.Key_) && this.Modifiers_.Equals(other.Modifiers_);
        }

        /// <summary>
        /// Computes and well formatted string that represents the current combination of Modifiers and Key.
        /// If one of Key or Modifiers is None the separator will be omitted.
        /// </summary>
        /// <param name="fmt">string argument for string.Format. Use {0} place holder for Modifiers, {1} for Key, {2} for separator.</param>
        /// <param name="separator">The string to use as separator when both Modifiers and Key are not None.</param>
        /// <returns></returns>
        public string ToString(string fmt, string separator)
        {
            string sep="", k="", m="";
            if (Modifiers_ != ModifierKeys.None && Key_ != Key.None)
            {
                sep = separator;
            }
            if (Key_ != Key.None)
            {
                k = Key_.ToString();
            }
            if (Modifiers_ != ModifierKeys.None)
            {
                m = Modifiers_.ToString();
            }
            return string.Format(fmt, m, k, sep);
        }

        public override string ToString()
        {
            return this.ToString("{0}{2}{1}", " + ");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fmt">string argument for string.Format. Use {0} place holder for Modifiers, {1} for Key.</param>
        /// <returns></returns>
        public string ToString(string fmt)
        {
            return ToString(fmt + "{2}", "");
        }
    }
}
