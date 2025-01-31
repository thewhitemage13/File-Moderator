namespace File_Moderator
{

    /// <summary>
    /// Головний клас програми для моніторингу активності користувача.
    /// </summary>
    internal static class Program
    {
        private static Mutex mutex = new Mutex(true, "ForbiddenWordsFinderMutex");
        /// <summary>
        /// Точка входу в програму.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}