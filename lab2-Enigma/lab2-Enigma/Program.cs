namespace lab2_Enigma
{
    class Program
    {
        static void Main(string[] args)
        {
            string coding = ".txt";
            string filename = "hm";
            
            // Создаем новую энигму
            Enigma e1 = new Enigma(4);
            // Сохраняем ее состояние
            e1.SaveEnigmaState("e1state");

            // Шифруем файл
            e1.ProcessFile(filename + coding, filename + "_coded" + coding);

            // Создаем новую энигму из файла конфигурации первой
            Enigma e2 = new Enigma("e1state");
            // Дешифруем файл
            e2.ProcessFile(filename + "_coded" + coding, filename + "_decoded" + coding);
        }
    }
}
