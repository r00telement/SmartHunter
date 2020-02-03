﻿using SmartHunter.Core.Data;
using System;
using System.Collections.Generic;

namespace SmartHunter.Game.Data
{
    public class Player : Bindable
    {
        int m_Index;
        public int Index
        {
            get { return m_Index; }
            set { SetProperty(ref m_Index, value); }
        }

        string m_Name;
        public string Name
        {
            get { return m_Name; }
            set { SetProperty(ref m_Name, value); }
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

        readonly Queue<(DateTime, int)> m_damageHistory = new Queue<(DateTime, int)>();
        public Queue<(DateTime, int)> DamageHistory => m_damageHistory;

        double _damagePerSecond;
        public double DamagePerSecond
        {
            get { return _damagePerSecond; }
            set { SetProperty(ref _damagePerSecond, value); }
        }
    }
}
