export interface StorageModel {
    id: string
    name?: string
    address?: string
}

export interface StorageCreateModel {
    name?: string
    address?: string
}

export interface StorageUpdateModel {
    id: string
    name?: string
    address?: string
}