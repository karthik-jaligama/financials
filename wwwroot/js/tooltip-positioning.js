// Tooltip positioning for hover notes
document.addEventListener('DOMContentLoaded', function() {
    initializeTooltipPositioning();
});

// Re-initialize when Blazor updates the DOM
if (!window.tooltipObserver) {
    window.tooltipObserver = new MutationObserver(function(mutations) {
        for (const mutation of mutations) {
            if (mutation.addedNodes.length > 0) {
                initializeTooltipPositioning();
                break;
            }
        }
    });
    
    window.tooltipObserver.observe(document.body, {
        childList: true,
        subtree: true
    });
}

function initializeTooltipPositioning() {
    const wrappers = document.querySelectorAll('.hover-note-wrapper');
    
    wrappers.forEach(wrapper => {
        const icon = wrapper.querySelector('.hover-note-icon');
        const tooltip = wrapper.querySelector('.hover-note-tooltip');
        
        if (!icon || !tooltip) return;
        
        wrapper.addEventListener('mouseenter', function() {
            const rect = icon.getBoundingClientRect();
            tooltip.style.left = (rect.right + 8) + 'px';
            tooltip.style.top = (rect.top + rect.height / 2) + 'px';
            tooltip.style.transform = 'translateY(-50%)';
        });
    });
}

