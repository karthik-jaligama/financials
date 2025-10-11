// Input formatting utilities for financial tables
window.inputFormatting = {
    
    // Check if an input should have number formatting (has commas or looks like a number)
    isNumberInput: function(input) {
        const value = input.value;
        // Check if value contains commas (formatted number) or is purely numeric
        return /,/.test(value) || /^\d+(\.\d+)?$/.test(value);
    },
    
    // Prevent deletion of commas in number inputs and intelligently skip over them
    handleNumberKeyDown: function(event) {
        const input = event.target;
        
        // Only apply number restrictions if this looks like a number field
        if (!this.isNumberInput(input)) {
            return true; // Allow all input for text fields
        }
        
        const cursorPos = input.selectionStart;
        const cursorEnd = input.selectionEnd;
        const value = input.value;
        
        // Handle selection deletion
        if (cursorPos !== cursorEnd) {
            return true;
        }
        
        // Check if user is trying to delete a comma
        if (event.key === 'Backspace') {
            const charToDelete = value.charAt(cursorPos - 1);
            
            if (charToDelete === ',') {
                event.preventDefault();
                const newPos = cursorPos - 1;
                let skipPos = newPos;
                while (skipPos > 0 && value.charAt(skipPos - 1) === ',') {
                    skipPos--;
                }
                input.setSelectionRange(skipPos, skipPos);
                return false;
            }
        } else if (event.key === 'Delete') {
            const charToDelete = value.charAt(cursorPos);
            
            if (charToDelete === ',') {
                event.preventDefault();
                const newPos = cursorPos + 1;
                let skipPos = newPos;
                while (skipPos < value.length && value.charAt(skipPos) === ',') {
                    skipPos++;
                }
                input.setSelectionRange(skipPos, skipPos);
                return false;
            }
        }
        
        // Only allow numbers, decimal point, and control keys for number inputs
        const allowedKeys = ['Backspace', 'Delete', 'Tab', 'Enter', 'ArrowLeft', 'ArrowRight', 'Home', 'End', 'Escape'];
        if (!allowedKeys.includes(event.key) && !/[\d.]/.test(event.key) && !event.ctrlKey && !event.metaKey) {
            event.preventDefault();
            return false;
        }
    },
    
    // Handle date input formatting with smart slash handling and placeholder masking
    handleDateKeyDown: function(event) {
        const input = event.target;
        const cursorPos = input.selectionStart;
        const cursorEnd = input.selectionEnd;
        const value = input.value;
        
        // Handle selection deletion
        if (cursorPos !== cursorEnd) {
            event.preventDefault();
            // Replace all selected characters with placeholders
            let newValue = value.substring(0, cursorPos);
            for (let i = cursorPos; i < cursorEnd; i++) {
                if (value.charAt(i) !== '/') {
                    newValue += this.getPlaceholderForPosition(i, value);
                } else {
                    newValue += '/';
                }
            }
            newValue += value.substring(cursorEnd);
            input.value = newValue;
            input.setSelectionRange(cursorPos, cursorPos);
            return false;
        }
        
        // Handle Backspace
        if (event.key === 'Backspace') {
            event.preventDefault();
            if (cursorPos === 0) {
                return false;
            }
            
            const charToDelete = value.charAt(cursorPos - 1);
            
            if (charToDelete === '/') {
                input.setSelectionRange(cursorPos - 1, cursorPos - 1);
            } else if (/\d/.test(charToDelete)) {
                const placeholder = this.getPlaceholderForPosition(cursorPos - 1, value);
                const newValue = value.substring(0, cursorPos - 1) + placeholder + value.substring(cursorPos);
                input.value = newValue;
                input.setSelectionRange(cursorPos - 1, cursorPos - 1);
            } else if (/[mdy]/.test(charToDelete)) {
                input.setSelectionRange(cursorPos - 1, cursorPos - 1);
            }
            return false;
        }
        
        // Handle Delete
        if (event.key === 'Delete') {
            event.preventDefault();
            if (cursorPos >= value.length) {
                return false;
            }
            
            const charToDelete = value.charAt(cursorPos);
            
            if (charToDelete === '/') {
                input.setSelectionRange(cursorPos + 1, cursorPos + 1);
            } else if (/\d/.test(charToDelete)) {
                const placeholder = this.getPlaceholderForPosition(cursorPos, value);
                const newValue = value.substring(0, cursorPos) + placeholder + value.substring(cursorPos + 1);
                input.value = newValue;
                input.setSelectionRange(cursorPos, cursorPos);
            } else if (/[mdy]/.test(charToDelete)) {
                input.setSelectionRange(cursorPos + 1, cursorPos + 1);
            }
            return false;
        }
        
        // Handle typing digits
        if (/\d/.test(event.key)) {
                event.preventDefault();
            
            // Validate the digit first
            if (!this.isValidDateDigit(event.key, cursorPos, value)) {
                return false;
            }
            
            const charAtCursor = value.charAt(cursorPos);
            
            // Skip slashes
            if (charAtCursor === '/') {
                const nextPos = cursorPos + 1;
                if (nextPos < value.length) {
                    const newValue = value.substring(0, nextPos) + event.key + value.substring(nextPos + 1);
                    const newCursorPos = (nextPos === 2 || nextPos === 5) ? nextPos + 2 : nextPos + 1;
                    input.value = newValue;
                    input.setSelectionRange(newCursorPos, newCursorPos);
                    
                    // Check if date is complete
                    if (this.isDateComplete(newValue)) {
                        this.triggerChangeEvent(input);
                    }
                }
            } else {
                const newValue = value.substring(0, cursorPos) + event.key + value.substring(cursorPos + 1);
                let newCursorPos = cursorPos + 1;
                if (newCursorPos < value.length && value.charAt(newCursorPos) === '/') {
                    newCursorPos++;
                }
                input.value = newValue;
                input.setSelectionRange(newCursorPos, newCursorPos);
                
                // Check if date is complete
                if (this.isDateComplete(newValue)) {
                    this.triggerChangeEvent(input);
                }
            }
            return false;
        }
        
        // Only allow control keys
        const allowedKeys = ['Backspace', 'Delete', 'Tab', 'Enter', 'ArrowLeft', 'ArrowRight', 'Home', 'End', 'Escape'];
        if (!allowedKeys.includes(event.key) && !event.ctrlKey && !event.metaKey) {
            event.preventDefault();
            return false;
        }
        
    },
    
    // Get the appropriate placeholder letter for a position in the date string
    getPlaceholderForPosition: function(pos, value) {
        // Count non-slash characters before position
        let digitIndex = 0;
        for (let i = 0; i < pos && i < value.length; i++) {
            if (value.charAt(i) !== '/') {
                digitIndex++;
            }
        }
        
        if (digitIndex < 2) return 'm'; // Month
        if (digitIndex < 4) return 'd'; // Day
        return 'y'; // Year
    },
    
    // Validate if a digit can be entered at the current position
    isValidDateDigit: function(digit, cursorPos, value) {
        
        // Count non-slash characters before cursor
        let digitIndex = 0;
        for (let i = 0; i < cursorPos && i < value.length; i++) {
            if (value.charAt(i) !== '/') {
                digitIndex++;
            }
        }
        
        
        // Get current date parts
        const parts = value.split('/');
        const monthPart = parts[0] || 'MM';
        const dayPart = parts[1] || 'DD';
        const yearPart = parts[2] || 'YY';
        
        
        // Month validation (positions 0-1)
        if (digitIndex === 0) {
            // First digit of month: must be 0 or 1
            if (digit > '1') {
                return false;
            }
            // If there's already a digit in position 2, check if combination is valid
            const secondMonthDigit = monthPart.charAt(1);
            if (/\d/.test(secondMonthDigit)) {
                const month = parseInt(digit + secondMonthDigit);
                if (month < 1 || month > 12) {
                    return false;
                }
            }
        } else if (digitIndex === 1) {
            // Second digit of month
            const firstMonthDigit = monthPart.charAt(0);
            if (/\d/.test(firstMonthDigit)) {
                const month = parseInt(firstMonthDigit + digit);
                if (month < 1 || month > 12) {
                    return false;
                }
            } else {
                // First digit is placeholder, any digit 0-9 is acceptable for now
                // (will be validated when first digit is entered)
            }
        }
        // Day validation (positions 2-3)
        else if (digitIndex === 2) {
            // First digit of day: must be 0-3
            if (digit > '3') {
                return false;
            }
            // If there's already a digit in position 4, check if combination is valid
            const secondDayDigit = dayPart.charAt(1);
            if (/\d/.test(secondDayDigit)) {
                const day = parseInt(digit + secondDayDigit);
                if (day < 1 || day > 31) {
                    return false;
                }
                // Check against month if it's valid
                const month = parseInt(monthPart);
                if (!isNaN(month)) {
                    // February
                    if (month === 2 && day > 29) {
                        return false;
                    }
                    // Months with 30 days (Apr, Jun, Sep, Nov)
                    if ([4, 6, 9, 11].includes(month) && day > 30) {
                        return false;
                    }
                }
            }
        } else if (digitIndex === 3) {
            // Second digit of day
            const firstDayDigit = dayPart.charAt(0);
            if (/\d/.test(firstDayDigit)) {
                const day = parseInt(firstDayDigit + digit);
                if (day < 1 || day > 31) {
                    return false;
                }
                // Check month-specific day limits
                const month = parseInt(monthPart);
                if (!isNaN(month)) {
                    // February
                    if (month === 2 && day > 29) {
                        return false;
                    }
                    // Months with 30 days (Apr, Jun, Sep, Nov)
                    if ([4, 6, 9, 11].includes(month) && day > 30) {
                        return false;
                    }
                }
            } else {
                // First digit is placeholder, any digit 0-9 is acceptable for now
                // (will be validated when first digit is entered)
            }
        }
        // Year validation (positions 4-5)
        else if (digitIndex === 4 || digitIndex === 5) {
            // Any digit 0-9 is valid for year
        }
        
        return true;
    },
    
    // Check if date is complete and valid
    isDateComplete: function(value) {
        const parts = value.split('/');
        if (parts.length !== 3) return false;
        
        const month = parts[0];
        const day = parts[1];
        const year = parts[2];
        
        // Check if all parts are digits (no placeholders)
        if (/[mdy]/.test(month) || /[mdy]/.test(day) || /[mdy]/.test(year)) {
            return false;
        }
        
        return true;
    },
    
    // Trigger change event for Blazor to pick up
    triggerChangeEvent: function(input) {
        const event = new Event('change', { bubbles: true });
        input.dispatchEvent(event);
    },
    
    // Initialize date input with template
    initializeDateInput: function(input) {
        if (!input.value || input.value.length === 0) {
            input.value = 'mm/dd/yy';
        }
    },
    
    // Delegated event handler for keydown
    handleKeyDown: function(event) {
        const target = event.target;
        
        // Check if target is a date input
        if (target.classList.contains('date-input')) {
            window.inputFormatting.handleDateKeyDown(event);
        }
        // Check if target is an editable cell (but not a date input)
        else if (target.classList.contains('editable-cell') && !target.classList.contains('date-input')) {
            window.inputFormatting.handleNumberKeyDown(event);
        }
    },
    
    // Initialize input formatting with event delegation
    initializeInputFormatting: function() {
        // Initialize date inputs with template
        document.querySelectorAll('.date-input').forEach(input => {
            this.initializeDateInput(input);
        });
    }
};

// Set up event delegation - only once
if (!window.inputFormattingInitialized) {
    window.inputFormattingInitialized = true;
    
    // Use event delegation for keydown events (attached once to document)
    document.addEventListener('keydown', window.inputFormatting.handleKeyDown, true);

// Initialize when DOM is loaded
    if (document.readyState === 'loading') {
document.addEventListener('DOMContentLoaded', function() {
    window.inputFormatting.initializeInputFormatting();
});
    } else {
        window.inputFormatting.initializeInputFormatting();
    }
    
    // Re-initialize date inputs when new content is added (for Blazor)
    const observer = new MutationObserver(function(mutations) {
        let shouldReinitialize = false;
        for (const mutation of mutations) {
            if (mutation.addedNodes.length > 0) {
                for (const node of mutation.addedNodes) {
                    if (node.nodeType === 1) {
                        if (node.matches && node.matches('.date-input')) {
                            shouldReinitialize = true;
                            break;
                        }
                        if (node.querySelector && node.querySelector('.date-input')) {
                            shouldReinitialize = true;
                            break;
                        }
                    }
                }
            }
            if (shouldReinitialize) break;
        }
        
        if (shouldReinitialize) {
            window.inputFormatting.initializeInputFormatting();
        }
    });
    
    observer.observe(document.body, {
        childList: true,
        subtree: true
    });
}
