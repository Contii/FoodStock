# FoodStock

FoodStock is a home inventory management system designed to optimize the organization of products, shopping lists, and meal preparation. Developed as a set of microservices, the system offers an interactive interface for both Web and Android devices.

This document aims to illustrate the overall scope of the system and its main functionalities, as well as the technologies that will be used in its development.

---

## System Objective

- **Organization**: Facilitate the management of food and household products, allowing precise control of items in stock.
- **Planning**: Assist in creating shopping lists and developing personalized menus based on available ingredients.
- **Waste Reduction**: Minimize food waste through alerts about products nearing expiration and recipe suggestions. (todo)

---

## Main Functionalities

- **Complete CRUD**: Registration, reading, updating, and deletion of items, with detailed information such as name, quantity, expiration date, and category.
- **Interface**: Easy and quick access to information, both for manual database control and everyday use of the application, through a responsive web interface.
- **QR Reader batch post**: Upload your new products directly via the QR code or Json lists on your receipts. (todo)
- **Reports**: Reports for out-of-stock or soon-to-expire products.
- **Shopping List Suggestions**: Creation of personalized lists based on missing products and the user's spending intentions. (todo)
- **Consumption Monitoring**: Consumption record of each item in stock, filtered by years or months.

---

## Technologies

- **Microsservices**: Modular architecture that allows scalability and independent maintenance of each system component.
- **Used Technologies**:
  - DOT.NET EFCore 8.0
  - Blazor Wasm
  - Docker

---

## Target Audience

- **Housekeepers**: Facilitating meal planning and food organization.
- **Students**: Assisting in budget management and healthy eating.
- **Families**: Facilitating the visualization and organization of products in general.

---

## Conclusion

FoodStock can enhance the way people manage their consumable stocks at home. With an intuitive interface and useful features, the system offers a solution for those seeking practicality, organization, and savings.

This system is part of the semester evaluation for the **Technologies in System Development** course taught by **Prof. Dr. Everton Coimbra de Araújo** at the **Federal Technological University of Paraná - Medianeira Campus**. 