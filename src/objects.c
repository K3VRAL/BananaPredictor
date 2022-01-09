#include "objects.h"

char *getType(Types type) {
    return  type == circle   ?   "cirle"   :
            type == slider   ?   "slider"  :
            type == spinner  ?   "spinner" :
                                 "isnull";
}

int ll_length(Node *head) {
    int len = 0;
    Node *current;
    for (current = head; current != NULL; current = current->next) {
        len++;
    }
    return len;
}

Node *ll_add(Node *head, TagID tagid, Tag tag) {
    Node *link = malloc(sizeof(Node));
    link->tagid = tagid;
    link->tag = tag;
    link->next = head;
    head = link;
    return head;
}

Node *ll_remove(Node *head, int key) {
    Node *current = head;
    int currkey = 0;
    Node *previous = NULL;
    if (head == NULL) {
        return head;
    }
    while (currkey != key) {
        if (current->next == NULL) {
            return head;
        }
        previous = current;
        current = current->next;
        currkey++;
    }
    if (current == head) {
        head = head->next;
    } else {
        previous->next = current->next;
    }
    free(current->line);
    free(current);
    return head;
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

Node *ll_sort(Node *head, char *delim) {
    Node *current;
    Node *next;
    int size = ll_length(head);
    for (int i = 0, k = size; i < size - 1; i++, k--) {
        current = head;
        next = head->next;
        for (int j = 1; j < k; j++) {
            char *tempcurr = strdup(current->line);
            char *tempnext = strdup(next->line);
            strtok(tempcurr, delim);
            strtok(NULL, delim);
            int tokencurr = atoi(strtok(NULL, delim));
            strtok(tempnext, delim);
            strtok(NULL, delim);
            int tokennext = atoi(strtok(NULL, delim));
            if (tokencurr > tokennext) {
                char *tempdata = strdup(current->line);
                free(current->line);
                current->line = strdup(next->line);
                free(next->line);
                next->line = strdup(tempdata);
                free(tempdata);
            }
            free(tempnext);
            free(tempcurr);
            current = current->next;
            next = next->next;
        }
    }
    return head;
}

Node *ll_free(Node *head) {
    for (int i = 0, size = ll_length(head); i < size; i++) {
        head = ll_remove(head, 0);
    }
    return head;
}