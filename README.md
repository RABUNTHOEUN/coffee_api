"# coffee_api" 

## Relation
### Summary of Relationships:
#### Users and Orders: One-to-many (A user can place many orders).
#### Orders and Order_Items: One-to-many (Each order can have many items).
#### Products and Order_Items: Many-to-many (Each product can appear in many orders, and each order can contain many products).
#### Orders and Payments: One-to-one (Each order has one payment).
#### Products and Inventory: One-to-one (Each product has a corresponding inventory entry).
#### Users and Reviews: One-to-many (A user can leave many reviews).
#### Products and Reviews: One-to-many (A product can have many reviews).