using System;
using System.Collections.Generic;
using System.Linq;
using ExternalServices;
using PharmacyLogic.Database;
using PharmacyServices;

//commento inserito per fare delle modifiche
namespace PharmacyLogic
{
    public class Pharmacy : IPharmacy
    {
        private static Pharmacy _instance;

        private Pharmacy()
        {
            using (var db = new PharmacyContext())
            {
                db.Database.Initialize(true);

                var stock = db.Stocks.Create();
                stock.Price = 12.56f;
                stock.MedicineName = "Cortison Chemicetina";
                stock.NextSupply = new DateTime(2015, 6, 16);
                stock.Quantity = 30;
                db.Stocks.Add(stock);

                var stock1 = db.Stocks.Create();
                stock1.Price = 23.56f;
                stock1.MedicineName = "Cortinal Areosol";
                stock1.NextSupply = new DateTime(2015, 6, 20);
                stock1.Quantity = 0;
                db.Stocks.Add(stock1);


                var stock2 = db.Stocks.Create();
                stock2.Price = 42f;
                stock2.MedicineName = "Proctidol";
                stock2.NextSupply = new DateTime(2015, 6, 18);
                stock2.Quantity = 2;
                db.Stocks.Add(stock2);

                db.SaveChanges();
            }
        }

        public static IPharmacy GetInstance()
        {
            if (_instance != null)
                return _instance;

            _instance = new Pharmacy();
            return _instance;
        }


        public bool WithdrawFromStocks(IList<RequestForOrderDTO.OrderedMedicine> medicines)
        {
            if (!medicines.Any())
                return true;

            using (var db = new PharmacyContext())
            {
                foreach (var med in medicines)
                {
                    var stock = db.Stocks.FirstOrDefault(s => s.MedicineName.Equals(med.MedicineName));
                    if (stock == null)
                        return false;

                    if (stock.Quantity >= med.Quantity)
                    {
                        stock.Quantity -= med.Quantity;
                        db.SaveChanges();
                    }

                    else if (!med.KnownIfIsNotAvailable)
                        return false;

                }
                return true;
            }
        }

        public IDictionary<string, IList<MedicineDTOPharmacy>> GetMedicinesForPrescription(IDictionary<string, Tuple<IList<MedicineDTO>, int>> medicines)
        {
            var availabilities = new Dictionary<string, IList<MedicineDTOPharmacy>>();
            foreach (var principle in medicines.Keys)
            {
                var availableMedicines = new List<MedicineDTOPharmacy>();
                var meds = medicines[principle];
                foreach (var m in meds.Item1)
                {
                    MedicineDTOPharmacy res;
                    if (CheckAvailability(m, meds.Item2, out res))
                    {
                        availableMedicines.Add(res);
                    }
                }
                availabilities.Add(principle, availableMedicines);
            }
            return availabilities;
        }

        private static bool CheckAvailability(MedicineDTO m, int quantity, out MedicineDTOPharmacy res)
        {
            using (var db = new PharmacyContext())
            {
                var stock = db.Stocks.FirstOrDefault(s => s.MedicineName == m.Name);
                if (stock == null)
                {
                    res = null;
                    return false;
                }

                res = new MedicineDTOPharmacy()
                {
                    ActivePrincipleName = m.ActivePrincipleName,
                    MedicineId = m.MedicineId,
                    Name = m.Name,
                    NextSupply = stock.NextSupply,
                    Price = stock.Price,
                    Availability = stock.Quantity >= quantity
                };
                return true;

            }
        }


    }
}
