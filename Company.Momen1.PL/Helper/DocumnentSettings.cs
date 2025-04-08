namespace Company.Momen1.PL.Helper
{
    public static class DocumnentSettings
    {
        // 1. Upload
        // ImageNAme
        public static string UploadFile(IFormFile file , string folderName)
        {
            // 1. Get Folder Location
            //string solderPath = "C:\\Users\\Amr\\source\\repos\\Company.Momen1\\Company.Momen1.PL\\wwwroot\\Files\\"+folderName;
            //var folderPath=  Directory.GetCurrentDirectory()+ "\\wwwroot\\Files\\"+ folderName;

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(),@"wwwroot\Files", folderName);

            // 2. GEt File Name And MAke IT Unique

            var fileName = $"{Guid.NewGuid()}{file.FileName}";

            //File Path

            var filePath = Path.Combine(folderPath,fileName);

            using var fileStream = new FileStream(filePath,FileMode.Create);

            file.CopyTo(fileStream);

            return fileName;     

        }
        // 1. Delete

        public static void DeleteFile(string fileName,string folderName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files", folderName,fileName);

            if (File.Exists(filePath))
                File.Delete(filePath);
        }

    }
}
