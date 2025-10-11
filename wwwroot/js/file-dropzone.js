window.initializeFileDropZone = (dotNetRef, elementId) => {
    const element = document.getElementById(elementId);
    if (!element) return;

    let dragCounter = 0;

    const handleDragEnter = (e) => {
        e.preventDefault();
        e.stopPropagation();
        dragCounter++;
        if (dragCounter === 1) {
            dotNetRef.invokeMethodAsync('SetDragActive', true);
        }
    };

    const handleDragLeave = (e) => {
        e.preventDefault();
        e.stopPropagation();
        dragCounter--;
        if (dragCounter === 0) {
            dotNetRef.invokeMethodAsync('SetDragActive', false);
        }
    };

    const handleDragOver = (e) => {
        e.preventDefault();
        e.stopPropagation();
    };

    const handleDrop = (e) => {
        e.preventDefault();
        e.stopPropagation();
        dragCounter = 0;
        dotNetRef.invokeMethodAsync('SetDragActive', false);

        const files = e.dataTransfer.files;
        const items = e.dataTransfer.items;
        
        console.log('=== DRAG AND DROP DEBUG INFO ===');
        console.log('Files array:', files);
        console.log('Items array:', items);
        console.log('DataTransfer types:', e.dataTransfer.types);
        console.log('DataTransfer effectAllowed:', e.dataTransfer.effectAllowed);
        console.log('DataTransfer dropEffect:', e.dataTransfer.dropEffect);
        
        if (files && files.length > 0) {
            // Log detailed file information
            Array.from(files).forEach((file, index) => {
                console.log(`File ${index + 1}:`, {
                    name: file.name,
                    type: file.type,
                    size: file.size,
                    lastModified: file.lastModified,
                    lastModifiedDate: file.lastModifiedDate,
                    webkitRelativePath: file.webkitRelativePath || 'N/A',
                    // These properties don't exist but let's check:
                    path: file.path || 'NOT AVAILABLE',
                    fullPath: file.fullPath || 'NOT AVAILABLE',
                    fileName: file.fileName || 'NOT AVAILABLE'
                });
            });
            
            // Check items for additional data
            if (items) {
                Array.from(items).forEach((item, index) => {
                    console.log(`Item ${index + 1}:`, {
                        kind: item.kind,
                        type: item.type,
                        // Try to get string data
                        getAsString: 'Available (async)'
                    });
                    
                    // Try to get string data if available
                    if (item.kind === 'string') {
                        item.getAsString((str) => {
                            console.log(`Item ${index + 1} string data:`, str);
                        });
                    }
                });
            }
            
            // Just capture metadata, not the actual file content
            const fileList = Array.from(files).map(file => ({
                name: file.name,
                type: file.type,
                size: file.size,
                lastModified: file.lastModified
            }));
            
            console.log('Sending to Blazor:', fileList);
            dotNetRef.invokeMethodAsync('HandleDroppedFiles', fileList);
        }
        
        console.log('=== END DEBUG INFO ===');
    };

    element.addEventListener('dragenter', handleDragEnter);
    element.addEventListener('dragleave', handleDragLeave);
    element.addEventListener('dragover', handleDragOver);
    element.addEventListener('drop', handleDrop);

    // Return cleanup function
    return () => {
        element.removeEventListener('dragenter', handleDragEnter);
        element.removeEventListener('dragleave', handleDragLeave);
        element.removeEventListener('dragover', handleDragOver);
        element.removeEventListener('drop', handleDrop);
    };
};

// Global function to handle file drops without element ID
window.handleFileDrop = (dotNetRef) => {
    return (e) => {
        e.preventDefault();
        e.stopPropagation();
        
        const files = e.dataTransfer.files;
        if (files && files.length > 0) {
            const fileList = Array.from(files).map(file => ({
                name: file.name,
                type: file.type,
                size: file.size,
                lastModified: file.lastModified
            }));
            dotNetRef.invokeMethodAsync('HandleDroppedFiles', fileList);
        }
    };
};
