// Write your JavaScript code here.
// This file is for custom JavaScript functionality for your Car Rental application.

// Example: Basic form validation and UI interactions
document.addEventListener('DOMContentLoaded', function() {
    // Add any custom JavaScript functionality here
    console.log('Car Rental application loaded');
    
    // Example: Form validation
    const forms = document.querySelectorAll('.needs-validation');
    Array.from(forms).forEach(form => {
        form.addEventListener('submit', event => {
            if (!form.checkValidity()) {
                event.preventDefault();
                event.stopPropagation();
            }
            form.classList.add('was-validated');
        }, false);
    });
});
