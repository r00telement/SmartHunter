using SmartHunter.Core.Data;
using System;

namespace SmartHunter.Game.Data
{
    public class Player : Bindable
    {
        string m_Name;
        public string Name
        {
            get { return m_Name; }
            set
            {
                if (SetProperty(ref m_Name, value))
                {
                    NotifyPropertyChanged(nameof(IsActive));
                }
            }
        }

        int m_Damage;
        public int Damage
        {
            get { return m_Damage; }
            set { SetProperty(ref m_Damage, value); }
        }

        float m_DamageFraction;
        public float DamageFraction
        {
            get { return m_DamageFraction; }
            set { SetProperty(ref m_DamageFraction, value); }
        }

        float m_BarFraction;
        public float BarFraction
        {
            get { return m_BarFraction; }
            set { SetProperty(ref m_BarFraction, value); }
        }

        public bool IsActive
        {
            get { return String.IsNullOrEmpty(Name) ? false : true; }
        }
    }
}
