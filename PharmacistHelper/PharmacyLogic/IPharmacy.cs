using System;
using System.Collections.Generic;

namespace PharmacyLogic
{
    public interface IPharmacy
    {
        /// <summary>
        /// Returns all the orders with expire date after the present day
        /// </summary>
        /// <returns></returns>
        ICollection<OrderDto> GetOrders();


        /// <summary>
        /// Checks if in stocks there is the quantity of the medicine specified 
        /// </summary>
        /// <param name="medicine"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Throws if medicine is null</exception>
        /// <exception cref="ArgumentException">Throws if quantity is less than zero</exception>
        bool CheckStocks(string medicine, int quantity);

        ISet<MedicineDto> GetGenerics(string activePrinciple); 


        /// <summary>
        /// Withdraws the quantity of medicine from stocks. If the operation succedes returns true, otherwise returns false.
        /// </summary>
        /// <param name="medicine"></param>
        /// <param name="quantity"></param>
        /// <exception cref="ArgumentNullException">Throws if medicine is null</exception>
        /// <exception cref="ArgumentException">Throws if quantity is less than zero</exception>
        bool WithdrawFromStocks(string medicine, int quantity);
    }
}