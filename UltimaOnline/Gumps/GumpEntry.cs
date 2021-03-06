using System;
using UltimaOnline.Network;

namespace UltimaOnline.Gumps
{
    public abstract class GumpEntry
    {
        private Gump m_Parent;

        protected GumpEntry()
        {
        }

        protected void Delta(ref int var, int val)
        {
            if (var != val)
            {
                var = val;

                if (m_Parent != null)
                {
                    m_Parent.Invalidate();
                }
            }
        }

        protected void Delta(ref bool var, bool val)
        {
            if (var != val)
            {
                var = val;

                if (m_Parent != null)
                {
                    m_Parent.Invalidate();
                }
            }
        }

        protected void Delta(ref string var, string val)
        {
            if (var != val)
            {
                var = val;

                if (m_Parent != null)
                {
                    m_Parent.Invalidate();
                }
            }
        }

        public Gump Parent
        {
            get
            {
                return m_Parent;
            }
            set
            {
                if (m_Parent != value)
                {
                    if (m_Parent != null)
                        m_Parent.Remove(this);

                    m_Parent = value;

                    if (m_Parent != null)
                        m_Parent.Add(this);
                }
            }
        }

        public abstract string Compile();
        public abstract void AppendTo(IGumpWriter disp);
    }
}