using System;
using UnityEngine;

namespace Shopping
{
    public class Wallet : MonoBehaviour
    {
        [SerializeField]
        private float startMoney = 200;

        private float money;

        public float CurrentMoney => money;

        public float SpentMoney { get; private set; }

        public event Action<float> OnMoneyChanged;

        private void Awake()
        {
            money = startMoney;
        }

        public void Initialize(float value)
        {
            startMoney = value;
            money = value;

            OnMoneyChanged?.Invoke(money);
        }

        public bool CanSpend(float amount)
        {
            return money >= amount;
        }

        public bool Spend(float amount)
        {
            if (money < amount)
                return false;

            money -= amount;
            SpentMoney += amount;

            OnMoneyChanged?.Invoke(money);

            return true;
        }
    }
}