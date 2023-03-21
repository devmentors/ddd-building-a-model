# Requirements

## Domain

Computer hardware store (e-commerce, sorry 💔)

## Requirements

- ***max. 2 items of each product***
    - **[motivation]** otherwise you're not individual customer but wholesale customer and you should go through different process
- ****order value not greater than 20000 PLN for cash payment***
    - **[motivation]** this rule is enforced by Polish law that requires cashless payment for B2C transactions above this value 
        - [similiar case] there is also 15000 PLN cash payment limit for B2B transactions that we can include here too or leave it as separate feature
- ***products with limited availability have limit 1 per customer per quoter***
    - **[motivation]** to avoid "scalping" practice by resellers, that purchase products like Sony PlayStation 5 console or NVIDIA RTX graphic cards and then resell them for much higher price (goal: fair chances for each customer)
