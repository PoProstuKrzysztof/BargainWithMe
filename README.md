# BARGAIN WITH ME

A small API application where customer can offer a new price for product.

Requirements for this project:

1. Creating the API of the Product Catalog Module - Implementing the ability to view products (list), add products, remove products. 

2. Create the API of the Negotiation module - Implement the described price negotiation process. 
3. Api Web Interface must be REST compliant. 
4.  Every Request to this application should be verified e.g. the proposed price must be greater than 0.

## Important
 Before any actions, you have to migrate database by using command in CLI "Update-database".

The database is empty to show the entire functionality of the api from adding a catalog, to negotiating the price of a product. 
## API Reference


### Catalogs

#### Get all catalogs

```http
  GET /api/catalog
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `-` | `Catalog` | Get all catalogs.  |

#### Get catalog

```http
  GET /api/catalog/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. Id of catalog to fetch |

#### Get catalog with all produtcs attached to it


```http
  GET /api/CatalogWithProdutcs/${id}
```
| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | Get catalog with all produtcs attached to it |



#### Create catalog

```http
  POST /api/catalog
  ```
  Example schema

```
{
  "id": "1fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "Catalog1"
}
```
#### Update catalog
```http
  PUT /api/catalog/${id}
```

Example schema 
```
{
  "id": "1fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "Catalog12"
}
```
#### Delete catalog

```http
  DELETE /api/catalog/${id}
```
| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. Id of catalog to delete |

### Products

#### Get all products

```http
  GET /api/product
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `-` | `Product` | Get all produtcs.  |

#### Get product

```http
  GET /api/product/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. Id of product to fetch |



#### Create product

```http
  POST /api/product
  ```
  Example schema

```
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "Telephone1",
  "price": {
    "amount": 10.3
  },
  "onStock": 4,
  "catalogId": "1fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```
#### Update product
```http
  PUT /api/product/${id}
```

Example schema 
```
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "Telephone1",
  "price": {
    "amount": 10.3
  },
  "onStock": 10,
  "catalogId": "1fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```
#### Delete product

```http
  DELETE /api/product/${id}
```
| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. Id of product to delete |


#### Negotiation 


```http
  POST /api/NegotiationPrice/${productId}/${proposedPrice}
  ```
  | Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `productId`      | `string` | **Required**. Id of the product whose price you wish to negotiate  |
| `proposedPrice`      | `double` | **Required**. Double value of proposed price |



