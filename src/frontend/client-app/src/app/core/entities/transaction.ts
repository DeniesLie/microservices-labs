import { TransactionType } from "../enums/TransactionType"
import { Item } from "./item"

export interface Transaction {
    id: string
    item: Item
    amount: number
    storageId: string
    type: TransactionType
    notes: string
}

export interface TransactionCreate {
    itemId: string
    amount: number
    storageId: string
    type: TransactionType
    notes?: string
}

export interface TransactionUpdate {
    id: string
    notes: string
}