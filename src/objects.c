#include "objects.h"

int ll_length(Node *head) {
    int len = 0;
    Node *current;
    for (current = head; current != NULL; current = current->next) {
        len++;
    }
    return len;
}

void ll_add(Node **head, TagID tagid, Tag tag) {
    Node *link = malloc(sizeof(Node));
    link->tagid = tagid;
    link->tag = tag;
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
    // if (current->tagid == aho) {} // TODO make a check if hsample is used and free it
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

void ll_sort(Node **head) {
    Node *current;
    Node *next;
    int size = ll_length(*head);
    for (int i = 0, k = size; i < size - 1; i++, k--) {
        current = *head;
        next = (*head)->next;
        for (int j = 1; j < k; j++) {
            int currtemp, nexttemp;
            switch ((*head)->tagid) {
                case inp:
                    currtemp = current->tag.inp.listM.startTime;
                    nexttemp = next->tag.inp.listM.startTime;
                    break;
                case atp:
                    currtemp = current->tag.atp.time;
                    nexttemp = next->tag.atp.time;
                    break;
                case aho:
                    currtemp = current->tag.atp.time;
                    nexttemp = next->tag.atp.time;
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