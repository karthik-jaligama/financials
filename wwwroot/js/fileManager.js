window.createBlobUrl = (fileData, contentType) => {
    const blob = new Blob([fileData], { type: contentType });
    return URL.createObjectURL(blob);
};

window.triggerFileInput = (element) => {
    if (element) {
        element.click();
    }
};

window.openFileInNewTab = (url) => {
    window.open(url, '_blank');
};

window.revokeBlobUrl = (url) => {
    URL.revokeObjectURL(url);
};

window.setupDragAndDrop = (dotNetRef) => {
    const dropZone = document.querySelector('.file-drop-zone');
    
    if (dropZone) {
        // Only prevent defaults on the drop zone, not the entire document
        ['dragenter', 'dragover', 'dragleave', 'drop'].forEach(eventName => {
            dropZone.addEventListener(eventName, preventDefaults, false);
        });
        
        // Handle drag enter and leave with debouncing
        let dragTimeout;
        ['dragenter', 'dragover'].forEach(eventName => {
            dropZone.addEventListener(eventName, () => {
                clearTimeout(dragTimeout);
                dotNetRef.invokeMethodAsync('SetDragActive', true);
            }, false);
        });
        
        ['dragleave', 'drop'].forEach(eventName => {
            dropZone.addEventListener(eventName, () => {
                clearTimeout(dragTimeout);
                dragTimeout = setTimeout(() => {
                    dotNetRef.invokeMethodAsync('SetDragActive', false);
                }, 50);
            }, false);
        });
        
        // Handle file drop
        dropZone.addEventListener('drop', async (e) => {
            try {
                const files = Array.from(e.dataTransfer.files);
                if (files.length === 0) return;
                
                const fileNames = files.map(f => f.name);
                const contentTypes = files.map(f => f.type);
                const fileDataArrays = [];
                
                for (const file of files) {
                    const arrayBuffer = await file.arrayBuffer();
                    const uint8Array = new Uint8Array(arrayBuffer);
                    fileDataArrays.push(Array.from(uint8Array));
                }
                
                // Use a small delay to prevent connection issues
                setTimeout(() => {
                    dotNetRef.invokeMethodAsync('HandleDroppedFiles', fileNames, fileDataArrays, contentTypes);
                }, 100);
            } catch (error) {
                console.error('Error handling file drop:', error);
            }
        }, false);
    }
};

function preventDefaults(e) {
    e.preventDefault();
    e.stopPropagation();
}

window.cleanupDragAndDrop = () => {
    const dropZone = document.querySelector('.file-drop-zone');
    if (dropZone) {
        // Remove all event listeners
        ['dragenter', 'dragover', 'dragleave', 'drop'].forEach(eventName => {
            dropZone.removeEventListener(eventName, preventDefaults, false);
        });
    }
};
