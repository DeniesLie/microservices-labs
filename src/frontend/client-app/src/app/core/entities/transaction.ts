export interface Transaction {
    id: string
    name?: string
    itemId: number
    itemName?: string
    itemAmount: number
    storageId: number
    notes: string
}

export interface TransactionCreate {
    name?: string
    itemId: number
    itemName?: string
    itemAmount: number
    storageId: number
    notes: string
}

export interface TransactionUpdate {
    id: string
    notes: string
}