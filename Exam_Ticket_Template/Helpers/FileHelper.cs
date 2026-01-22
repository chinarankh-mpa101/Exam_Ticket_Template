namespace Exam_Ticket_Template.Helpers
{
    public static class FileHelper
    {

        public static async Task<string> FileUploadAsync(this IFormFile file,string folderPath)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + file.FileName;
            string path = Path.Combine(folderPath, uniqueFileName);
            using FileStream stream = new(path, FileMode.Create);
            await file.CopyToAsync(stream);
            return uniqueFileName;
        }
    }
}
