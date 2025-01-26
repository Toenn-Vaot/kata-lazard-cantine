# Kata - Gestion de Cantine

## Feature 1 - Gestion de facturation d'un repas
Le programme permet de :

- Créditer le compte d'un client de son budget pour la cantine
- Payer le plateau repas du client, et pour cela :
  - Débiter le compte du montant du repas
  - Générer un "ticket client" dans lequel devra figurer :
        - le détail des produits ajoutés au plateau
        - le total à régler par le client

## Règles
**Règle 1** - Chaque client peut avoir un prix de plateau fixe de 10€ s'il est composé comme suit

- 1 Entrée
- 1 Plat
- 1 Dessert
- 1 Pain 

**Règle 2** - Chaque produit peut être acheté via un supplément, en fonction du type de produit

- Boisson : 1€
- Fromage : 1€
- Pain : 0,40€
- Petit Salade Bar : 4€
- Grand Salade Bar : 6€
- Portion de fruit : 1€
- Entrée supplementaire : 3€
- Plat supplementaire : 6€
- Dessert supplementaire : 3€ 

**Règle 3** - Le ticket du client prend en compte la prise en charge employeur, en fonction du type de client
 
- Client Interne : 7.5€ de prise en charge
- Client Prestataire : 6€ de prise en charge
- Client VIP : 100%
- Client Stagiaire : 10€
- Client Visiteur : pas de prise en charge

**Règle 4** - Le paiement du repas devra être bloqué si le montant à débiter est supérieur au crédit restant sur le compte du client sauf pour les internes et VIP pour lesquels le découvert est autorisé.

## API - Scénario
### Créer le ticket pour un client
- Choisir un client dans la liste **HTTP GET** `/customers`
- Créer le ticket avec le client choisi **HTTP POST** `/cantine/tickets/customer/{customerId}`
- __Mémoriser le numéro de ticket donné__

### Ajouter/Retirer des produits dans le ticket
- Avec le numéro de ticket précédemment créé et l'identifiant du produit choisi dans la liste **HTTP GET** `/products`
- Ajouter un produit avec la quantité souhaitée avec **HTTP POST** `/cantine/tickets/{id}/products/{productId}/add/{quantity}`
- et/ou retirer un produit avec la quantité souhaitée avec **HTTP POST** `/cantine/tickets/{id}/products/{productId}/add/{quantity}
- Observer le contenu et le statut du ticket avec **HTTP GET** `/tickets/{id}`

### Payer le ticket
- Avant de payer, il faut recupérer l'argent du jour avec **HTTP POST** `/customers/{id}/purse/daily`. (Cela alimente le compte d'un valeur  aléatoire)
- Observer le **solde du client** avec **HTTP GET** `/customers/{id}`
- Observer le **montant du ticket** avec **HTTP GET** `/tickets/{id}`
- Procéder ensuite au paiement avec **HTTP POST** `/cantine/tickets/{id}/pay`
- Observer le contenu et le statut du ticket avec **HTTP GET** `/tickets/{id}`