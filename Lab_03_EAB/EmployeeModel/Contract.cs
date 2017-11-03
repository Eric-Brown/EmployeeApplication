﻿using System;
using System.Runtime.Serialization;

namespace Lab_03_EAB
{
    [DataContract]
    [Serializable]
    public sealed class Contract : Employee
    {
        [DataMember]
        private const string CONTRACT_FORMAT_STRING = "Contract Wage: {0}\n",
            BAD_WAGE_CONSTR = "Only non-negative values of wage may be used to construct a Contract employee.";
        /// <summary>
        /// ContractWage property and backing field. Negative values are rejected.
        /// </summary>
        [DataMember]
        private decimal contractWage;
        public Decimal ContractWage
        {
            get => contractWage;
            set
            {
                if (value >= 0)
                    contractWage = value;
            }
        }
        /// <summary>
        /// Constructor. Hands parameters to "Employee"'s constructor.
        /// </summary>
        /// <param name="id">The Employees Identifiying Number</param>
        /// <param name="first">The Employees first name</param>
        /// <param name="last">The Employees last name</param>
        /// <param name="wage">The Employees contracted wage</param>
        public Contract(uint id, string first, string last, Decimal wage)
            :base(id, ETYPE.CONTRACT , first,last)
        {
            if (wage < 0) throw new ArgumentException(BAD_WAGE_CONSTR);
            ContractWage = wage;
        }
        /// <summary>
        /// Gives a string representation of the class
        /// </summary>
        /// <returns>A string representing the current state of the class</returns>
        public override string ToString()
        {
            return base.ToString() + string.Format(CONTRACT_FORMAT_STRING, ContractWage);
        }
    }
}
