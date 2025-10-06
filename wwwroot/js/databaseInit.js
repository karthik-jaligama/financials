/**
 * Database Initialization Script
 * This script handles one-time database initialization and data seeding
 */

class DatabaseInitializer {
    constructor() {
        this.isInitialized = false;
        this.initKey = 'financials_db_initialized';
    }

    /**
     * Check if database has been initialized
     */
    checkInitializationStatus() {
        return localStorage.getItem(this.initKey) === 'true';
    }

    /**
     * Mark database as initialized
     */
    markAsInitialized() {
        localStorage.setItem(this.initKey, 'true');
        this.isInitialized = true;
    }

    /**
     * Initialize database with seed data
     */
    async initializeDatabase() {
        if (this.checkInitializationStatus()) {
            console.log('Database already initialized');
            return;
        }

        try {
            console.log('Initializing database with seed data...');
            
            // Call the backend initialization endpoint
            const response = await fetch('/api/database/initialize-database', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                }
            });

            if (response.ok) {
                this.markAsInitialized();
                console.log('Database initialized successfully');
                
                // Show success message to user
                this.showNotification('Database initialized successfully!', 'success');
            } else {
                const errorText = await response.text();
                console.error('Database initialization failed:', errorText);
                throw new Error(`HTTP error! status: ${response.status}, message: ${errorText}`);
            }
        } catch (error) {
            console.error('Failed to initialize database:', error);
            // Don't show error notification if it's a network error during page load
            if (document.readyState === 'complete') {
                this.showNotification('Failed to initialize database. Please refresh the page.', 'error');
            }
        }
    }

    /**
     * Reset database (for development purposes)
     */
    async resetDatabase() {
        try {
            console.log('Resetting database...');
            
            const response = await fetch('/api/database/reset-database', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                }
            });

            if (response.ok) {
                localStorage.removeItem(this.initKey);
                this.isInitialized = false;
                console.log('Database reset successfully');
                this.showNotification('Database reset successfully!', 'success');
                
                // Reload the page to reflect changes
                setTimeout(() => {
                    window.location.reload();
                }, 2000);
            } else {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
        } catch (error) {
            console.error('Failed to reset database:', error);
            this.showNotification('Failed to reset database.', 'error');
        }
    }

    /**
     * Show notification to user
     */
    showNotification(message, type = 'info') {
        // Create notification element
        const notification = document.createElement('div');
        notification.className = `notification notification-${type}`;
        notification.textContent = message;
        
        // Style the notification
        notification.style.cssText = `
            position: fixed;
            top: 20px;
            right: 20px;
            padding: 12px 20px;
            border-radius: 4px;
            color: white;
            font-weight: 500;
            z-index: 10000;
            max-width: 300px;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
            transition: all 0.3s ease;
        `;

        // Set background color based on type
        switch (type) {
            case 'success':
                notification.style.backgroundColor = '#10b981';
                break;
            case 'error':
                notification.style.backgroundColor = '#ef4444';
                break;
            case 'warning':
                notification.style.backgroundColor = '#f59e0b';
                break;
            default:
                notification.style.backgroundColor = '#3b82f6';
        }

        // Add to page
        document.body.appendChild(notification);

        // Remove after 5 seconds
        setTimeout(() => {
            notification.style.opacity = '0';
            notification.style.transform = 'translateX(100%)';
            setTimeout(() => {
                if (notification.parentNode) {
                    notification.parentNode.removeChild(notification);
                }
            }, 300);
        }, 5000);
    }

    /**
     * Add initialization button to the page
     */
    addInitializationButton() {
        // Check if button already exists
        if (document.getElementById('db-init-button')) {
            return;
        }

        const button = document.createElement('button');
        button.id = 'db-init-button';
        button.textContent = 'Initialize Database';
        button.style.cssText = `
            position: fixed;
            bottom: 20px;
            right: 20px;
            padding: 10px 20px;
            background-color: #3b82f6;
            color: white;
            border: none;
            border-radius: 6px;
            cursor: pointer;
            font-size: 14px;
            font-weight: 500;
            z-index: 1000;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
            transition: all 0.2s ease;
        `;

        button.addEventListener('mouseenter', () => {
            button.style.backgroundColor = '#2563eb';
            button.style.transform = 'translateY(-2px)';
        });

        button.addEventListener('mouseleave', () => {
            button.style.backgroundColor = '#3b82f6';
            button.style.transform = 'translateY(0)';
        });

        button.addEventListener('click', () => {
            this.initializeDatabase();
        });

        document.body.appendChild(button);
    }

    /**
     * Add reset button to the page (for development)
     */
    addResetButton() {
        // Check if button already exists
        if (document.getElementById('db-reset-button')) {
            return;
        }

        const button = document.createElement('button');
        button.id = 'db-reset-button';
        button.textContent = 'Reset Database';
        button.style.cssText = `
            position: fixed;
            bottom: 70px;
            right: 20px;
            padding: 10px 20px;
            background-color: #ef4444;
            color: white;
            border: none;
            border-radius: 6px;
            cursor: pointer;
            font-size: 14px;
            font-weight: 500;
            z-index: 1000;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
            transition: all 0.2s ease;
        `;

        button.addEventListener('mouseenter', () => {
            button.style.backgroundColor = '#dc2626';
            button.style.transform = 'translateY(-2px)';
        });

        button.addEventListener('mouseleave', () => {
            button.style.backgroundColor = '#ef4444';
            button.style.transform = 'translateY(0)';
        });

        button.addEventListener('click', () => {
            if (confirm('Are you sure you want to reset the database? This will delete all data.')) {
                this.resetDatabase();
            }
        });

        document.body.appendChild(button);
    }
}

// Initialize when DOM is loaded
document.addEventListener('DOMContentLoaded', () => {
    try {
        const dbInitializer = new DatabaseInitializer();
        
        // Add buttons for database management
        dbInitializer.addInitializationButton();
        dbInitializer.addResetButton();
        
        // Auto-initialize if not already done
        if (!dbInitializer.checkInitializationStatus()) {
            console.log('Database not initialized, auto-initializing...');
            // Add a small delay to ensure the page is fully loaded
            setTimeout(() => {
                dbInitializer.initializeDatabase();
            }, 1000);
        }
    } catch (error) {
        console.error('Error initializing database script:', error);
    }
});

// Export for use in other scripts
window.DatabaseInitializer = DatabaseInitializer;
