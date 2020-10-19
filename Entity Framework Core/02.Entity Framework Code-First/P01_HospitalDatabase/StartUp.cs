namespace P01_HospitalDatabase
{
    using P01_HospitalDatabase.Data;
    using P01_HospitalDatabase.UserControl;
    
    class Program
    {
        static void Main(string[] args)
        {
            HospitalContex context = new HospitalContex();

            HospitalUserInterface hospitalUserInterface = new HospitalUserInterface();

            hospitalUserInterface.Start(context);

        }
    }
}
