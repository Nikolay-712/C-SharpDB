namespace P01_HospitalDatabase.UserControl
{
    using P01_HospitalDatabase.Data;
    using P01_HospitalDatabase.Data.Models;
    using System;
    using System.Linq;
    public class HospitalUserInterface
    {
        public void Start(HospitalContex context)
        {
            var input = Console.ReadLine();

            while (input != "Stop")
            {
                var inputArgs = input.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();

                switch (inputArgs[0])
                {
                    case "AddPatient":

                        var patient = new Patient()
                        {
                            FirstName = inputArgs[1],
                            LastName = inputArgs[2],
                            Address = inputArgs[3],
                            Email = inputArgs[4]
                        };

                        context.Patients.Add(patient);

                        break;
                    case "AddVisitation":

                        var info = inputArgs.Skip(2).ToArray();

                        int patientId = int.Parse(info.Last());
                        string comment = string.Join(" ", info.SkipLast(1));

                        var visitation = new Visitation()
                        {
                            Date = DateTime.Parse(inputArgs[1]),
                            Comments = comment,
                            PatientId = patientId
                        };

                        context.Visitations.Add(visitation);

                        break;
                    case "AddMedicament":

                        var medicament = new Medicament()
                        {
                            Name = inputArgs[1]
                        };

                        context.Medicaments.Add(medicament);

                        break;
                    case "AddDiagnose":

                        string name = inputArgs[1];
                        string diagnoseComment = 
                            string.Join(" ",inputArgs.Skip(2).SkipLast(1));

                        var targetPatientId = context.Patients
                            .Where(x => x.FirstName == inputArgs.Last())
                            .FirstOrDefault().PatientId;

                        var diagnose = new Diagnose()
                        {
                            Name = name,
                            Comments = diagnoseComment,
                            PatientId = targetPatientId
                        };

                        context.Diagnoses.Add(diagnose);

                        break;
                    case "AddedMedicationToPatient":

                       
                        var patientID = context
                                .Patients
                                .Where(x => x.FirstName == inputArgs[1])
                                .FirstOrDefault().PatientId;

                        var medicamentID = context
                            .Medicaments
                            .Where(x => x.Name == inputArgs[2])
                            .FirstOrDefault().MedicamentId;

                        var patientsMedicaments = new PatientMedicament()
                        {
                            PatientId = patientID,
                            MedicamentId = medicamentID

                        };

                        context.PatientMedicaments.Add(patientsMedicaments);
                        break;
                    default:
                        break;

                }

                context.SaveChanges();
                input = Console.ReadLine();
            }
        }
    }
}
