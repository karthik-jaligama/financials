import React from 'react';
import { X } from 'lucide-react';

interface FileItemProps {
  file: {
    id: string;
    name: string;
    type: string;
    url: string;
  };
  onRemove: (id: string) => void;
}

const getFileIcon = (fileType: string, fileName: string) => {
  const extension = fileName.split('.').pop()?.toLowerCase();
  
  if (fileType.includes('pdf') || extension === 'pdf') {
    return (
      <div className="w-12 h-12 bg-red-100 border border-red-200 rounded flex items-center justify-center">
        <div className="text-red-600 text-xs font-semibold">PDF</div>
      </div>
    );
  }
  
  if (fileType.includes('image')) {
    return (
      <div className="w-12 h-12 bg-blue-100 border border-blue-200 rounded flex items-center justify-center">
        <div className="text-blue-600 text-xs font-semibold">IMG</div>
      </div>
    );
  }
  
  if (fileType.includes('word') || extension === 'doc' || extension === 'docx') {
    return (
      <div className="w-12 h-12 bg-blue-100 border border-blue-200 rounded flex items-center justify-center">
        <div className="text-blue-600 text-xs font-semibold">DOC</div>
      </div>
    );
  }
  
  if (fileType.includes('excel') || extension === 'xls' || extension === 'xlsx') {
    return (
      <div className="w-12 h-12 bg-green-100 border border-green-200 rounded flex items-center justify-center">
        <div className="text-green-600 text-xs font-semibold">XLS</div>
      </div>
    );
  }
  
  // Default file icon
  return (
    <div className="w-12 h-12 bg-gray-100 border border-gray-200 rounded flex items-center justify-center">
      <div className="text-gray-600 text-xs font-semibold">FILE</div>
    </div>
  );
};

export const FileItem: React.FC<FileItemProps> = ({ file, onRemove }) => {
  const handleDoubleClick = () => {
    // Open file in new tab/window
    window.open(file.url, '_blank');
  };

  return (
    <div className="relative group">
      <button
        className="absolute -top-2 -right-2 w-5 h-5 bg-gray-400 hover:bg-gray-600 rounded-full flex items-center justify-center opacity-0 group-hover:opacity-100 transition-opacity z-10"
        onClick={() => onRemove(file.id)}
        title="Remove file"
      >
        <X className="w-3 h-3 text-white" />
      </button>
      
      <div 
        className="flex flex-col items-center cursor-pointer hover:opacity-80 transition-opacity"
        onDoubleClick={handleDoubleClick}
        title={`Double-click to open ${file.name}`}
      >
        {getFileIcon(file.type, file.name)}
        <div className="mt-1 text-xs text-center text-gray-700 max-w-16 truncate">
          {file.name}
        </div>
      </div>
    </div>
  );
};