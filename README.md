# Topic: Application for managing sport events
## Subject: Integrated systems

### __Overview__

SportEventManagement is a .NET Core-based application designed to facilitate ticket purchases and provide an admin interface for managing event registrations. The application follows Onion Architecture for clean, maintainable, and scalable development, and is hosted on Microsoft Azure with an Azure PostgreSQL database for data storage.

### __Features__

* Ticket Purchasing and Upcoming Events: Users can browse upcoming sports events and easily purchase tickets through the integrated Stripe payment gateway.
* Event Registration and Approval: Users can register for sports events, and administrators can review and approve registrations via the admin panel.
* External API Integration: The app communicates with external APIs to ensure up-to-date event data and real-time updates.
* Email Notifications: The application includes an email service to notify users about ticket purchases in which the tickets are contained into the attachment.
* Excel Export/Import for Tickets: Using the EPPlus library, users can export tickets to Excel files and import tickets from Excel files, allowing for flexible ticket management and reporting.

### __ETL Process with Azure Data Factory__
The application incorporates an ETL (Extract, Transform, Load) process in collaboration with another team using Azure Data Factory. This allows for efficient processing of data related to ticket sales and event metrics for business insights.

### __Architecture__

* Onion Architecture for a modular and layered design.
* Azure PostgreSQL as the main data storage solution.
* Hosted on Microsoft Azure to ensure scalability, high availability, and ease of management.


