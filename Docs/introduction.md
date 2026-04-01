# 1. Introduction

## 1.1 Project Overvie

The Smart Furniture Store System is a web-based platform designed to provide a modern and intelligent shopping experience for furniture products. The system allows customers to browse, explore, and purchase a wide range of furniture items through an easy-to-use and interactive interface.

This platform is designed not only as a traditional e-commerce store, but also as a smart system that enhances user experience by providing features such as product categorization, search functionality, personalized recommendations, and detailed product visualization.

The system supports three main types of users: Customers, who can browse and purchase furniture; Sellers, who can manage and upload their furniture products; and Administrators, who oversee the entire system, manage users, and monitor sales and performance.

The Smart Furniture Store aims to simplify the process of buying and selling furniture online by providing a centralized platform that is efficient, scalable, and user-friendly. It integrates frontend and backend technologies with a well-structured database system to ensure smooth operations and secure transactions.

This project demonstrates the practical implementation of software engineering principles, including system analysis, system design, database modeling, API development, and testing, resulting in a complete and functional real-world application.

## 1.2 Problem Statement

| **Category**            | **Description**                                                                                                                                                                                                                                                                                                                                                                                      |
| ----------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Current Situation**   | Customers often face difficulty when searching for suitable furniture online due to scattered platforms, lack of proper categorization, and limited visualization of products. Furniture sellers also struggle to present their products effectively and manage inventory in an organized way. Additionally, administrators lack centralized tools to monitor sales and user activities efficiently. |
| **Issues Identified**   | • Difficulty in finding suitable furniture due to poor search and filtering options.<br>• Lack of detailed product visualization and information.<br>• Sellers have limited tools to manage inventory and showcase products.<br>• No centralized system for tracking orders and sales performance.<br>• Weak user experience in traditional furniture websites.                                      |
| **Impact**              | • Customers may leave without making a purchase due to confusion or lack of clarity.<br>• Sellers lose potential sales Due to the poor product supply..<br>• اA management that cannot make decisions based on clear data.<br>• The overall user experience is poor and uncomfortable..                                                                                                                                        |
| **Need for a Solution** | There is a need for a smart, centralized platform that allows customers to It makes browsing and selecting the right furniture easy, provides sellers with tools to manage their products, and gives management an integrated monitoring and reporting system, while improving the user experience through smart features such as advanced search and clear categorization..                                                                                                                     |
## Functional Requirements

**User Management**
- The system shall allow users to register accounts.
- The system shall allow users to log in and log out securely.
- The system shall support different roles (Customer, Seller, Admin).
- The system shall allow users to update their profile information.
- 
**Product Management**
- Sellers shall be able to add new furniture products.
- Sellers shall be able to edit product details.
- Sellers shall be able to delete products.
- The system shall display product details (name, price, description, images).
  
**Search & Filtering**
- Users shall be able to search for products by name.
- Users shall be able to filter products by:
- Category (e.g., sofa, table, bed)
- Price range
- Availability
  
**Shopping Cart**
- Customers shall be able to add products to cart.
- Customers shall be able to remove products from cart.
- The system shall calculate total price automatically.
  
**Order Management**
- Customers shall be able to place orders.
- Customers shall be able to track order status.
- Sellers shall receive order notifications.
- Sellers shall update order status (Processing / Shipped / Delivered).
  
**Payment System**
- The system shall allow multiple payment methods.
- The system shall validate payment transactions.
- The system shall handle failed payments.
  
**Review System**
- Customers shall be able to rate products.
- Customers shall be able to write reviews.
- The system shall display ratings and reviews.
  
**Admin Control Panel**
- Admin shall manage all users.
- Admin shall monitor orders.
- Admin shall manage products.
- Admin shall view analytics and reports.

## Non-Functional Requirements
**Performance**
- The system shall respond within 2–3 seconds.
- The system shall support multiple users simultaneously.
  
**Security**
- All user data shall be encrypted.
- Authentication and authorization must be implemented.
- The system shall prevent unauthorized access
  
**Usability**
- The system shall have a simple and user-friendly interface.
- The system shall be responsive (mobile + desktop).
  
**Reliability**
- The system shall be available 24/7.
- The system shall handle errors without crashing.
  
**Maintainability**
- The system shall be modular and easy to update.
- Code shall follow clean architecture principles.
  
 ## User Types (Actors)
- Customer → Browse & Buy furniture
- Seller → Manage products & orders
- Admin → Full system control
  
##  System Features Summary
- Online furniture marketplace
- Multi-vendor system
- Smart search & filtering
- Order tracking system
- Dashboard for all users




## 1.3 Project Scope

**In Scope (Version 1.0):**
The Smart Furniture Store system will include the following features:

- User registration and login functionality (Customers, Sellers, Admins)
- Customer ability to browse, search, and filter furniture products
- Viewing detailed product information (images, price, description, category)
- Shopping cart and checkout process
- Order placement and order tracking
- Seller dashboard to manage products (add, update, delete)
- Inventory management for sellers
- Admin dashboard to manage users, products, and orders
- Basic reporting and analytics (sales, orders, users)
- Responsive web design for usability across devices.

**Out of Scope:**
The following features are not included in this project:

- Integration with real payment gateways (will be simulated only)
- Advanced AI-based recommendations (basic logic only if implemented)
- Augmented Reality (AR) furniture preview داخل الغرفة
- Mobile application (focus will be on web application only)
- Delivery logistics and real-time shipment tracking systems
- Multi-language support (optional if time يسمح)
- 
## 1.4 Project Objectives

| **Objective**                                               | **Success Metric**                                            |
| ----------------------------------------------------------- | ------------------------------------------------------------- |
| Develop a user-friendly Smart Furniture Store platform      | Users can easily browse products and complete the purchase process without complications. |
| Enable customers to search and filter furniture efficiently |Reduce search time and increase speed in accessing the desired product.            |
| Provide sellers with tools to manage products and inventory |The seller's ability to add and modify products easily and without errors     |
| Implement a complete shopping cart and checkout system      |Successful purchase transactions completed without system issues                 |
| Build an admin dashboard for system monitoring              |The administration's ability to control users, requests, and display reports   |
| Ensure system performance and usability                     |The system operates quickly and stably without any apparent errors during use.   |
| Apply software engineering best practices                   | Full documentation + organized design + clear code                    |
| Deliver a complete and functional graduation project        |A complete and ready-to-present project that meets the requirements of the material.                |


## 1.5 Methodology

Development Approach

The project will follow the Agile Software Development Life Cycle (SDLC) methodology. Agile is chosen because it allows iterative development, continuous feedback, and rapid adaptation to changes, which is ideal for web-based e-commerce systems.

Agile Process Flow
| **Phase**                       | **Description**                                                                                                                                  |
| ------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------ |
| **Requirement Analysis Phase**        | GThe Requirement Analysis Phase focuses on identifying, analyzing, and documenting the system requirements for the Smart Furniture Store. This phase ensures that the system meets the needs of all stakeholders, including customers, sellers, and administrators. |
| **System Design**               | The system architecture is designed, including: Database design (ER Diagram), System structure(Frontend & Backend), I/UX design (wireframes) |
| **Implementation**              | The system is developed using appropriate technologies: Frontend development (user interface), Backend development (business logic & APIs), Database implementation
Each team member works on their assigned tasks, and the system is built step by step.                |
| **Testing**                     | Conduct functional, usability, and performance testing. Identify and fix bugs iteratively. Ensure system meets all success metrics.              |
| **Deployment**                  | Deploy the system on a web server. Ensure all components work in a live environment.                                                             |
| **Maintenance & Documentation** | Monitor system performance, provide updates, and maintain technical and user documentation.                                                      |


## 1.6 Document Audience

| **Audience**                                         | **Purpose / Use of Document**                                                                                                                                      |
| ---------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| **Project Supervisor**                               | To review and evaluate the project structure, objectives, methodology, and deliverables. Ensure the project meets academic standards.                              |
| **Development Team (Frontend/Backend/Docs/Testers)** | To understand the project scope, system design, roles, and responsibilities. Serve as a reference for coding, implementation, and testing.                         |
| **End Users (Customers)**                            | To guide on how to navigate the system, register, browse products, place orders, track orders, and write reviews. Provided via User Manual.                        |
| **Sellers / Vendors**                                | To understand how to manage products, process orders, and interact with customers using the platform. Provided via User Manual.                                    |
| **Administrators**                                   | To guide on managing users, monitoring system activities, generating reports, and using the admin dashboard. Provided via User Manual and Technical Documentation. |
| **Future Developers / Maintenance Team**             | To understand system architecture, database design, code structure, and APIs for future updates or enhancements. Provided via Technical Documentation.             |


---

[← Back to README](README.md) | [Next: Stakeholders Analysis →](./02-stakeholders-analysis.md)
