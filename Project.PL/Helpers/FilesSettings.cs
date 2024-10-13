namespace Project.PL.Helpers
{
    public class FilesSettings
    {

        public static string UploadFile(IFormFile file, string folderName)
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);
            string fileName = $"{Guid.NewGuid()}{file.FileName}";
            string filepath = Path.Combine(folderPath, fileName);

            var fileStream = new FileStream(filepath, FileMode.Create);
            file.CopyTo(fileStream);
            fileStream.Close();
            return fileName;

        }

        public static void DeleteFile(string fileName, string folderName)
        {
            string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName, fileName);

            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
            else
            {
                GC.Collect();   
                GC.WaitForPendingFinalizers();
                Console.WriteLine("File not found: " + filepath);
            }

        }
    }
}