/* This file was generated by SableCC (http://www.sablecc.org/). */

package org.sablecc.sablecc.xss2.node;

import java.util.*;
import org.sablecc.sablecc.xss2.analysis.*;

public final class AVarXpathElem extends PXpathElem
{
    private TXvar _xvar_;

    public AVarXpathElem ()
    {
    }

    public AVarXpathElem (
            TXvar _xvar_
    )
    {
        setXvar (_xvar_);
    }

    public Object clone()
    {
        return new AVarXpathElem (
            (TXvar)cloneNode (_xvar_)
        );
    }

    public void apply(Switch sw)
    {
        ((Analysis) sw).caseAVarXpathElem(this);
    }

    public TXvar getXvar ()
    {
        return _xvar_;
    }

    public void setXvar (TXvar node)
    {
        if(_xvar_ != null)
        {
            _xvar_.parent(null);
        }

        if(node != null)
        {
            if(node.parent() != null)
            {
                node.parent().removeChild(node);
            }

            node.parent(this);
        }

        _xvar_ = node;
    }

    public String toString()
    {
        return ""
            + toString (_xvar_)
        ;
    }

    void removeChild(Node child)
    {
        if ( _xvar_ == child )
        {
            _xvar_ = null;
            return;
        }
    }

    void replaceChild(Node oldChild, Node newChild)
    {
        if ( _xvar_ == oldChild )
        {
            setXvar ((TXvar) newChild);
            return;
        }
    }

}
