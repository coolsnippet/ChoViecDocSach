2 ways:
    1. from edmx: it will create a connection string attach with something like :
            metadata=res://*/
    2. from codefirst:
            the connection string is better

However, if we want to convert from edmx to codefirst, we use
    1. SqlParameter instead of ObjectParameter
    2. IEnumerable instead of ObjectResult
    3. this.Database.SqlQuery<LPFDetail>("__GetmyLPF @mDate,@mAirline,@mSeg_fr,@mSeg_to,@mTail", mDateParameter, mAirlineParameter, mSeg_frParameter, mSeg_toParameter, mTailParameter);

Good article: https://damianbrady.com.au/2014/12/19/calling-stored-procedures-from-entity-framework-6-code-first/
and for meta data in connection string of #1: http://stackoverflow.com/questions/689355/metadataexception-unable-to-load-the-specified-metadata-resource
    