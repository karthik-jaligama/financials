import React from 'react';
import { RelatedFiles } from './components/RelatedFiles';

export default function App() {
  return (
    <div className="min-h-screen bg-gray-50 py-8">
      <div className="container mx-auto px-4">
        <div className="max-w-4xl mx-auto">
          <div className="bg-white rounded-lg shadow-sm border border-gray-200 p-6">
            <RelatedFiles />
          </div>
          
          {/* Instructions */}
          <div className="mt-6 text-center text-sm text-gray-600 space-y-1">
            <p>This CRM file manager allows you to drag and drop files without downloading them.</p>
            <p>Files are stored as references and can be opened by double-clicking their icons.</p>
          </div>
        </div>
      </div>
    </div>
  );
}