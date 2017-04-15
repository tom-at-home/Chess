using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Chess
{
    abstract class Chessman
    {
        protected bool isWhite;
        public bool IsWhite { get => isWhite; set => isWhite = value; }

        protected StackPanel view;
        public StackPanel View { get => view; set => view = value; }

        protected string current_position;
        public string Current_Position { get => current_position; set => current_position = value; }

        protected string desc;
        public string Desc
        {
            get { return desc; }
            set { desc = value; }
        }

        public abstract void Move(MyButton source, MyButton dest);

    }
}
