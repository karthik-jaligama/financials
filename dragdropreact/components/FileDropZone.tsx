import React, { useState, useCallback } from 'react';
import { Download } from 'lucide-react';

interface FileDropZoneProps {
  onFilesAdded: (files: File[]) => void;
}

export const FileDropZone: React.FC<FileDropZoneProps> = ({ onFilesAdded }) => {
  const [isDragActive, setIsDragActive] = useState(false);

  const handleDrag = useCallback((e: React.DragEvent) => {
    e.preventDefault();
    e.stopPropagation();
  }, []);

  const handleDragIn = useCallback((e: React.DragEvent) => {
    e.preventDefault();
    e.stopPropagation();
    if (e.dataTransfer.items && e.dataTransfer.items.length > 0) {
      setIsDragActive(true);
    }
  }, []);

  const handleDragOut = useCallback((e: React.DragEvent) => {
    e.preventDefault();
    e.stopPropagation();
    setIsDragActive(false);
  }, []);

  const handleDrop = useCallback((e: React.DragEvent) => {
    e.preventDefault();
    e.stopPropagation();
    setIsDragActive(false);
    
    if (e.dataTransfer.files && e.dataTransfer.files.length > 0) {
      const files = Array.from(e.dataTransfer.files);
      onFilesAdded(files);
    }
  }, [onFilesAdded]);

  return (
    <div
      className={`
        border-2 border-dashed rounded-lg p-12 text-center transition-colors
        ${isDragActive 
          ? 'border-blue-400 bg-blue-50' 
          : 'border-gray-300 bg-gray-50 hover:border-gray-400'
        }
      `}
      onDragEnter={handleDragIn}
      onDragLeave={handleDragOut}
      onDragOver={handleDrag}
      onDrop={handleDrop}
    >
      <Download className="w-8 h-8 text-gray-400 mx-auto mb-4" />
      <p className="text-gray-500">
        Drag and drop files here.
      </p>
    </div>
  );
};