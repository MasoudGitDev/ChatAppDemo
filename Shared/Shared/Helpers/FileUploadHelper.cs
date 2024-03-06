using Microsoft.AspNetCore.Http;
using Shared.Exceptions;
using Shared.Extensions;
using Shared.Models;

namespace Shared.Helpers;
public class FileUploadHelper {

    /// <summary>
    /// For 1Mb length you must use 1024*1024.
    /// <returns></returns>
    public static async Task<Result> UploadAsync(
        IFormFile file ,
        string[] permittedExtensions ,
        long permittedLength ,
        string folderPath) {
        CheckFormFile(file , permittedExtensions , permittedLength);
        return await SaveAsync(file , folderPath);
    }

    public static async Task<Result> DefaultUploadImageAsync(IFormFile file , string folderPath) {
        CheckFormFile(file , LegalImageExtensions , ToMegaBite(1));
        return await SaveAsync(file , folderPath);
    }

    //public static async Task<(bool flag, string message)> DefaultUploadAudioAsync(IFormFile file , string Id) {
    //    CheckFormFile(file , LegalAudioExtensions , ToMegaBite(10));
    //    return await SaveAsync(file , Id, @"Audios\");
    //}

    //public static async Task<(bool flag, string message)> DefaultUploadVideoAsync(IFormFile file , string Id) {
    //    CheckFormFile(file , LegalVideoExtensions , ToMegaBite(100));
    //    return await SaveAsync(file , Id, @"Videos\");
    //}

    public static long ToMegaBite(uint number) => number * 1024 * 1024;

    public static string[] LegalImageExtensions => [".jpg" , ".jpeg" , ".png" , ".gif"];
    public static string[] LegalVideoExtensions => [".mp4"];
    public static string[] LegalAudioExtensions => [".mp3"];


    // ===================== private methods
    private static void CheckFormFile(IFormFile file , string[] permittedExtensions , long permittedLength) {
        if(file == null || file.Length <= 0)
            throw new ArgumentNullException("There is no any file valid file to upload.");
        if(file.Length > permittedLength) {
            throw new IllegalException("Length" ,
                $"The length of the file must be less than or equal to {permittedLength}.");
        }
        var extension = Path.GetExtension(file.FileName);
        if(String.IsNullOrWhiteSpace(extension) || !permittedExtensions.Contains(extension)) {
            throw new IllegalException("Extension" ,
                "Just these extensions are valid : " + string.Join(',' , permittedExtensions));
        }
    }


    public static async Task<Result> SaveAsync(IFormFile file , string folderPath) {
        try {
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", folderPath, file.FileName);

            if(File.Exists(fullPath))
                throw new FoundException("This file name already exists.");

            Directory.CreateDirectory(Path.GetDirectoryName(fullPath).ThrowIfNull());
            using(var stream = File.Create(fullPath))
                await file.CopyToAsync(stream);

            return new Result(Enums.ResultStatus.Success , new("FileUpload" , "File uploaded successfully."));
        }
        catch(Exception ex) {
            return new Result(Enums.ResultStatus.Failed , new("FileUpload" , ex.Message));
        }
    }



}
