# Clasificador de Pedidos

## Descripción
Este proyecto implementa un sistema que clasifica pedidos de una tienda en línea y calcula el costo de envío según el monto de la compra, el tipo de cliente, la cantidad de productos y el destino.

## Entradas
- Monto del pedido  
- Ciudad destino  
- Tipo de cliente (nuevo o recurrente)  
- Cantidad de ítems  

## Proceso
El sistema evalúa reglas de negocio para asignar el tipo de envío (gratis, express o estándar) y aplica un costo adicional si el destino es al exterior.

## Salidas
- Categoría de despacho  
- Costo de envío  
- Mensaje para el cliente  

## Reglas
- Envío gratis si monto ≥ 150.000 y cliente recurrente.  
- Envío express si ítems ≥ 5 o monto ≥ 300.000.  
- Envío estándar en los demás casos.  
- Recargo adicional para envíos al exterior.
