using System;
using System.Linq;
using DataContext;

namespace TestRest.Services
{
    public class ScheduleService
    {
        private IMail _mail;

        private static ScheduleService _instance;
        public static ScheduleService Instance
        {
            get { return _instance ?? (_instance = new ScheduleService(new MailService())); }
        }

        public ScheduleService(IMail mail)
        {
            _mail = mail;
        }

        public void CheckServices()
        {
            CheckForServices();

            CheckForVaccinations();
        }

        private void CheckForServices()
        {
            using (var model = new PetHelperContext())
            {
                var begin = DateTime.Today;
                var end = DateTime.Today.AddDays(14);
                var services = model.ServicesForPets.Where(s => s.Date >= begin && s.Date < end).ToList();

                foreach (var service in services)
                {
                    var master = service.Pet.Master;
                    var body =
                        "Уважаем%Gender% %User%, через %Days% дней для Вашего питомца назначена услуга: %Service%. Пожалуйста, приходите вовремя! Мы Вас ждем!";
                    body = body.Replace("%Gender%", master.Gender ? "ый" : "ая");
                    body = body.Replace("%User%", master.Surname + " " + master.Name[0] + ".");
                    body = body.Replace("%Days%", (service.Date - DateTime.Today).Days.ToString());
                    body = body.Replace("%Service", service.Service.Name);

                    _mail.Send(master.Email, "Напоминание об услуге", body);
                }
            }
        }

        private void CheckForVaccinations()
        {
            using (var model = new PetHelperContext())
            {
                var begin = DateTime.Today;
                var end = DateTime.Today.AddDays(14);
                var vacs = model.PetsVaccinations.Where(s => s.Date >= begin && s.Date < end).ToList();

                foreach (var vac in vacs)
                {
                    var master = vac.Pet.Master;
                    var body =
                        "Уважаем%Gender% %User%, через %Days% дней для Вашего питомца назначена вакцина: %Service%. Пожалуйста, приходите вовремя! Мы Вас ждем!";
                    body = body.Replace("%Gender%", master.Gender ? "ый" : "ая");
                    body = body.Replace("%User%", master.Surname + " " + master.Name[0] + ".");
                    body = body.Replace("%Days%", (vac.Date - DateTime.Today).Days.ToString());
                    body = body.Replace("%Service", vac.Vaccination.Name);

                    _mail.Send(master.Email, "Напоминание об вакцинации", body);
                }
            }
        }
    }
}