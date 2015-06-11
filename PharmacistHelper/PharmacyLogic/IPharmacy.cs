﻿using System;
using System.Collections.Generic;
using ExternalServices;

namespace PharmacyLogic
{
    public interface IPharmacy
    {

        /// <summary>
        /// Withdraws the quantity of medicine from stocks. If the operation succedes returns true, otherwise returns false.
        /// </summary>
        /// <param name="medicine"></param>
        /// <param name="quantity"></param>
        /// <exception cref="ArgumentNullException">Throws if medicines is null</exception>
        /// <exception cref="ArgumentException">Throws if there is a quantity that is less than zero</exception>
        bool WithdrawFromStocks(IDictionary<MedicineDTO, int> medicines);

        /// <summary>
        /// Checks if the active principles in the prescriptions have some related medicine and if this medicine are present in the 
        /// stocks. Returns the map between the prescriptionID and the related medicines.
        /// </summary>
        /// <param name="prescriptions"></param>
        /// <returns></returns>
        IDictionary<string, IList<MedicineDTOPharmacy>> GetMedicinesForPrescription(
            IDictionary<string, Tuple<IList<MedicineDTO>, int>> medicines );


    }
}