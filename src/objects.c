#include "objects.h"

int ll_length(Node *head) {
    int len = 0;
    Node *current;
    for (current = head; current != NULL; current = current->next) {
        len++;
    }
    return len;
}

void ll_add(Node **head, TagID tagid, Tag tag, unsigned int id) {
    Node *link = malloc(sizeof(Node));
    link->tagid = tagid;
    link->tag = tag;
    link->id = id;
    link->next = *head;
    *head = link;
}

void ll_remove(Node **head, int key) {
    Node *current = *head;
    int currkey = 0;
    Node *previous = NULL;
    if (head == NULL) {
        return;
    }
    while (currkey != key) {
        if (current->next == NULL) {
            return;
        }
        previous = current;
        current = current->next;
        currkey++;
    }
    if (current == *head) {
        *head = (*head)->next;
    } else {
        previous->next = current->next;
    }
    free(current);
    return;
}

void ll_remove_byid(Node **head, unsigned int id) {
    Node *current = *head;
    Node *previous = NULL;
    if (head == NULL) {
        return;
    }
    while (current->id != id) {
        if (current->next == NULL) {
            return;
        }
        previous = current;
        current = current->next;
    }
    if (current == *head) {
        *head = (*head)->next;
    } else {
        previous->next = current->next;
    }
    free(current);
    return;
}

Node *ll_get(Node *head, int key) {
    Node *current = head;
    int currkey = 0;
    if (head == NULL) {
        return NULL;
    }
    while (currkey != key) {
        if (current->next == NULL) {
            return NULL;
        }
        current = current->next;
        currkey++;
    }
    return current;
}

Node *ll_get_byid(Node *head, unsigned int id) {
    Node *current = head;
    if (head == NULL) {
        return NULL;
    }
    while (current->id == id) {
        if (current->next == NULL) {
            return NULL;
        }
        current = current->next;
    }
    return current;
}

void ll_sort(Node **head) {
    Node *current;
    Node *next;
    int size = ll_length(*head);
    for (int i = 0, k = size; i < size - 1; i++, k--) {
        current = *head;
        next = (*head)->next;
        for (int j = 1; j < k; j++) {
            int currtemp, nexttemp;
            switch (current->tagid) {
                case inp:
                    switch (current->tag.inp.lid) {
                        case als:
                            currtemp = current->tag.inp.ls.als.startTime;
                            break;
                        case sls:
                            currtemp = current->tag.inp.ls.sls.time;
                            break;
                    }
                    break;
                case atp:
                    currtemp = current->tag.atp.time;
                    break;
                case aho:
                    currtemp = current->tag.aho.time;
                    break;
            }
            switch (next->tagid) {
                case inp:
                    switch (next->tag.inp.lid) {
                        case als:
                            nexttemp = next->tag.inp.ls.als.startTime;
                            break;
                        case sls:
                            nexttemp = next->tag.inp.ls.sls.time;
                            break;
                    }
                    break;
                case atp:
                    nexttemp = next->tag.atp.time;
                    break;
                case aho:
                    nexttemp = next->tag.aho.time;
                    break;
            }
            if (currtemp > nexttemp) {
                TagID temptid = current->tagid;
                Tag temptag = current->tag;
                current->tagid = next->tagid;
                current->tag = next->tag;
                next->tagid = temptid;
                next->tag = temptag;
            }
            current = current->next;
            next = next->next;
        }
    }
}

void ll_free(Node *head) {
    for (int i = 0, size = ll_length(head); i < size; i++) {
        ll_remove(&head, 0);
    }
}

int ll_giveid(Node *list) {
    bool exists = true;
    int id;
    while (exists) {
        exists = false;
        id = rand() % USHRT_MAX;
        if (ll_get_byid(list, id) != NULL) {
            exists = true;
        }
    }
    return id;
}