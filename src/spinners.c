#include "../include/spinners.h"

void addTiming(Node **llTP, int start, int end) {
    Node *current = *llTP;
    Tag tag;
    for (int i = 0; i < ll_length(current); i++) {

    }
    
    tag.atp.time = start;
    // TODO figure out what and how beatlength and meter works then implement
    // tag.atp.beatlength = ;
    // tag.atp.meter = ;
    tag.atp.sset = 2;
    tag.atp.sindex = 0;
    tag.atp.volume = 1;
    tag.atp.uninherited = 1;
    tag.atp.effects = 0;
    ll_add(&current, atp, tag, ll_giveid(current));

    tag = (const Tag){ 0 };
    // TODO for found uninherited point
    ll_add(&current, atp, tag, ll_giveid(current));
}

void addSpinners(Node **llHO, int start, int end, int distance) {
    Node *current = *llHO;
    Tag tag;
    for (int i = 0; i < ll_length(current); i++) {

    }
    
    for (int i = start; i < end; i += distance) {
        tag.aho.x = 256;
        tag.aho.y = 192;
        tag.aho.time = i;
        tag.aho.type = spinner;
        tag.aho.ncombo = true;
        tag.aho.hsound = 0;
        // tag.aho.hsample = ; // TODO first fix hitsample
        tag.aho.hobject.spinner.endtime = end + i;
        ll_add(&current, aho, tag, ll_giveid(current));
    }

    tag = (const Tag){ 0 };
    // TODO for found object
    ll_add(&current, aho, tag, ll_giveid(current));
}

void processSpinner(Node **llHO, listSpin data) {

}