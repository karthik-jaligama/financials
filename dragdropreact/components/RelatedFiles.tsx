import React, { useState } from 'react';
import { FolderOpen } from 'lucide-react';
import { FileDropZone } from './FileDropZone';
import { FileItem } from './FileItem';

interface FileData {
  id: string;
  name: string;
  type: string;
  url: string;
}

export const RelatedFiles: React.FC = () => {
  const [files, setFiles] = useState<FileData[]>([]);

  const handleFilesAdded = (newFiles: File[]) => {
    const fileData: FileData[] = newFiles.map(file => ({
      id: Math.random().toString(36).substr(2, 9),
      name: file.name,
      type: file.type,
      // Create a blob URL for the file - this creates a reference without downloading
      url: URL.createObjectURL(file)
    }));

    setFiles(prevFiles => [...prevFiles, ...fileData]);
  };

  const handleRemoveFile = (fileId: string) => {
    setFiles(prevFiles => {
      const fileToRemove = prevFiles.find(f => f.id === fileId);
      if (fileToRemove) {
        // Clean up the blob URL to prevent memory leaks
        URL.revokeObjectURL(fileToRemove.url);
      }
      return prevFiles.filter(f => f.id !== fileId);
    });
  };

  return (
    <div className="w-full max-w-2xl mx-auto p-6">
      {/* Header */}
      <div className="flex items-center gap-2 mb-6">
        <FolderOpen className="w-5 h-5 text-slate-600" />
        <h2 className="text-slate-700 font-medium">Related Files</h2>
      </div>

      {/* Main content area */}
      <div className="flex gap-6">
        {/* Drop Zone */}
        <div className="flex-1">
          <FileDropZone onFilesAdded={handleFilesAdded} />
        </div>

        {/* Files Display */}
        {files.length > 0 && (
          <div className="flex flex-wrap gap-4 items-start">
            {files.map(file => (
              <FileItem
                key={file.id}
                file={file}
                onRemove={handleRemoveFile}
              />
            ))}
          </div>
        )}
      </div>

      {/* Instructions */}
      {files.length > 0 && (
        <div className="mt-4 text-xs text-gray-500 text-center">
          Double-click any file to open it
        </div>
      )}
    </div>
  );
};