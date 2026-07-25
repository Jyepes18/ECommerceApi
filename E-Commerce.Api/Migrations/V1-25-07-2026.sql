CREATE TABLE role (
  id SERIAL PRIMARY KEY,
  role VARCHAR(50) NOT NULL
);

CREATE TABLE users (
   id SERIAL PRIMARY KEY,
   names VARCHAR(100) NOT NULL,
   last_name VARCHAR(100) NOT NULL,
   is_company BOOLEAN NOT NULL DEFAULT FALSE,
   email VARCHAR(150) NOT NULL UNIQUE,
   password VARCHAR(255) NOT NULL,
   nit VARCHAR(30),
   name_company VARCHAR(150),
   role_id INT NOT NULL,

   CONSTRAINT fk_users_role
       FOREIGN KEY (role_id)
           REFERENCES role(id)
);

CREATE TABLE product (
     id SERIAL PRIMARY KEY,
     name VARCHAR(150) NOT NULL,
     description TEXT,
     price DECIMAL(10,2) NOT NULL,
     quantity INT NOT NULL,
     user_id INT NOT NULL,
    
     CONSTRAINT fk_product_user
         FOREIGN KEY (user_id)
             REFERENCES users(id)
);

CREATE TABLE cart (
  id SERIAL PRIMARY KEY,
  user_id INT NOT NULL,
  created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

  CONSTRAINT fk_cart_user
      FOREIGN KEY (user_id)
          REFERENCES users(id)
);

CREATE TABLE cart_items (
    id SERIAL PRIMARY KEY,
    cart_id INT NOT NULL,
    product_id INT NOT NULL,
    quantity INT NOT NULL,
    
    CONSTRAINT fk_cart_items_cart
        FOREIGN KEY (cart_id)
            REFERENCES cart(id),
    
    CONSTRAINT fk_cart_items_product
        FOREIGN KEY (product_id)
            REFERENCES product(id)
);

CREATE TABLE orders (
    id SERIAL PRIMARY KEY,
    user_id INT NOT NULL,
    total DECIMAL(10,2) NOT NULL,
    status VARCHAR(50) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_orders_user
        FOREIGN KEY (user_id)
            REFERENCES users(id)
);

CREATE TABLE order_items (
 id SERIAL PRIMARY KEY,
 order_id INT NOT NULL,
 product_id INT NOT NULL,
 quantity INT NOT NULL,
 price DECIMAL(10,2) NOT NULL,
 subtotal DECIMAL(10,2) NOT NULL,

 CONSTRAINT fk_order_items_order
     FOREIGN KEY (order_id)
         REFERENCES orders(id),

 CONSTRAINT fk_order_items_product
     FOREIGN KEY (product_id)
         REFERENCES product(id)
);

CREATE TABLE payments (
  id SERIAL PRIMARY KEY,
  order_id INT NOT NULL,
  card_number VARCHAR(20),
  total DECIMAL(10,2) NOT NULL,
  status VARCHAR(50) NOT NULL,
  transaction_id VARCHAR(100),
  created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

  CONSTRAINT fk_payments_order
      FOREIGN KEY (order_id)
          REFERENCES orders(id)
);