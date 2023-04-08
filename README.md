﻿# Requirements

## Domain

Computer hardware store (e-commerce, sorry 💔)

## Commentary

🎦 YouTube (PL):
1. **_What is model?_**
   - 🔗 Link: https://www.youtube.com/watch?v=HJHmSH4X_dk
   - 📄 Projects: V1 - V5
2. **What is aggregate?**
    - 🔗 Link: https://www.youtube.com/watch?v=g6OVgndzYpQ
    - 📄 Projects: V6 - V7
3. **_Trade offs in model - DDD trilemma_**
    - 🔗 Link: TODO
    - 📄 Projects: V8 - V9

## Requirements

- ***max. 2 items of each product***
    - **[motivation]** otherwise you're not individual customer but wholesale customer and you should go through different process
- ****order value not greater than 20000 PLN for cash payment***
    - **[motivation]** this rule is enforced by Polish law that requires cashless payment for B2C transactions above this value
        - [similiar case] there is also 15000 PLN cash payment limit for B2B transactions that we can include here too or leave it as separate feature
- ***products with limited availability have limit 1 per customer per quarter***
    - **[motivation]** to avoid "scalping" practice by resellers, that purchase products like Sony PlayStation 5 console or NVIDIA RTX graphic cards and then resell them for much higher price (goal: fair chances for each customer)
