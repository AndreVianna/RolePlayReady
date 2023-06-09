﻿namespace System.Abstractions;

public interface IFileSystem {
    string CombinePath(params string[] paths);
    string[] GetFilesFrom(string folderPath, string searchPattern, SearchOption searchOptions);
    bool FileExists(string filePath);
    void CreateFolderIfNotExists(string folderPath);
    void MoveFile(string sourcePath, string targetPath);
    string GetFileNameFrom(string filePath);
    Stream OpenFileForReading(string filePath);
    Stream CreateNewFileAndOpenForWriting(string path);
}